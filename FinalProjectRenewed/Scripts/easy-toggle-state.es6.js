/**
 * -------------------------------------------------------------------
 * easy-toggle-state
 * A tiny JavaScript plugin to toggle the state of any HTML element in most of contexts with ease.
 *
 * @author Matthieu Bué <https://twikito.com>
 * @version v1.5.0
 * @link https://twikito.github.io/easy-toggle-state/
 * @license MIT : https://github.com/Twikito/easy-toggle-state/blob/master/LICENSE
 * -------------------------------------------------------------------
 */

(function () {
	'use strict';

	/**
	 * You can change this PREFIX value to prevent conflict with another JS library.
	 * This prefix will be set to all attributes like data-[PREFIX]-class.
	 */
	const PREFIX = "toggle";

	/* Retrieve a valid HTML attribute. */
	const dataset = (key => ["data", PREFIX, key].filter(Boolean).join("-"));

	/* HTML attributes */
	const CHECKED = "aria-checked",
	      CLASS = dataset("class"),
	      ESCAPE = dataset("escape"),
	      EVENT = dataset("event"),
	      EXPANDED = "aria-expanded",
	      GROUP = dataset("group"),
	      HIDDEN = "aria-hidden",
	      IS_ACTIVE = dataset("is-active"),
	      OUTSIDE = dataset("outside"),
	      SELECTED = "aria-selected",
	      TARGET = dataset("target"),
	      TARGET_ALL = dataset("target-all"),
	      TARGET_NEXT = dataset("target-next"),
	      TARGET_ONLY = dataset("target-only"),
	      TARGET_PARENT = dataset("target-parent"),
	      TARGET_PREVIOUS = dataset("target-previous"),
	      TARGET_SELF = dataset("target-self"),
	      TARGET_STATE = dataset("state"),
	      TRIGGER_OFF = dataset("trigger-off");

	/* Retrieve all triggers with a specific attribute */
	const $$ = ((selector, node) => {
		const scope = selector ? `[${selector}]` : "";
		return node ? [...node.querySelectorAll(scope)] : [...document.querySelectorAll(`[${CLASS}]${scope}`.trim())];
	});

	/* Manage ARIA attributes */
	const manageAria = ((element, config = {
		[CHECKED]: element.isToggleActive,
		[EXPANDED]: element.isToggleActive,
		[HIDDEN]: !element.isToggleActive,
		[SELECTED]: element.isToggleActive
	}) => {
		Object.keys(config).forEach(key => element.hasAttribute(key) && element.setAttribute(key, config[key]));
	});

	/* Retrieve all active trigger of a group. */
	const retrieveGroupActiveElement = (group => $$(`${GROUP}="${group}"`).filter(groupElement => groupElement.isToggleActive));

	/* Test the targets list */
	const testTargets = ((selector, targetList) => {

		/* Test if there's no match for a selector */
		if (targetList.length === 0) {
			console.warn(`There's no match for the selector '${selector}' for this trigger`);
		}

		/* Test if there's more than one match for an ID selector */
		const matches = selector.match(/#\w+/gi);
		if (matches) {
			matches.forEach(match => {
				const result = [...targetList].filter(target => target.id === match.slice(1));
				if (result.length > 1) {
					console.warn(`There's ${result.length} matches for the selector '${match}' for this trigger`);
				}
			});
		}

		return targetList;
	});

	/* Retrieve all targets of a trigger element. */
	const retrieveTargets = (element => {
		if (element.hasAttribute(TARGET) || element.hasAttribute(TARGET_ALL)) {
			const selector = element.getAttribute(TARGET) || element.getAttribute(TARGET_ALL);
			return testTargets(selector, document.querySelectorAll(selector));
		}

		if (element.hasAttribute(TARGET_PARENT)) {
			const selector = element.getAttribute(TARGET_PARENT);
			return testTargets(selector, element.parentElement.querySelectorAll(selector));
		}

		if (element.hasAttribute(TARGET_SELF)) {
			const selector = element.getAttribute(TARGET_SELF);
			return testTargets(selector, element.querySelectorAll(selector));
		}

		if (element.hasAttribute(TARGET_PREVIOUS)) {
			return testTargets("previous", [element.previousElementSibling].filter(Boolean));
		}

		if (element.hasAttribute(TARGET_NEXT)) {
			return testTargets("next", [element.nextElementSibling].filter(Boolean));
		}

		return [];
	});

	/* Toggle off all 'toggle-outside' elements when reproducing specified or click event outside trigger or target elements. */
	const documentEventHandler = event => {
		const target = event.target;
		if (!target.closest("[" + TARGET_STATE + '="true"]')) {
			$$(OUTSIDE).forEach(element => {
				if (element !== target && element.isToggleActive) {
					(element.hasAttribute(GROUP) ? manageGroup : manageToggle)(element);
				}
			});
			if (target.hasAttribute(OUTSIDE) && target.isToggleActive) {
				document.addEventListener(target.getAttribute(EVENT) || "click", documentEventHandler, false);
			}
		}
	};

	/* Manage click on 'trigger-off' elements. */
	const triggerOffHandler = event => {
		manageToggle(event.target.targetElement);
	};

	/* Manage attributes and events of target elements. */
	const manageTarget = (targetElement, triggerElement) => {
		targetElement.isToggleActive = !targetElement.isToggleActive;
		manageAria(targetElement);

		if (triggerElement.hasAttribute(OUTSIDE)) {
			targetElement.setAttribute(TARGET_STATE, triggerElement.isToggleActive);
		}

		const triggerOffList = $$(TRIGGER_OFF, targetElement);
		if (triggerOffList.length > 0) {
			if (triggerElement.isToggleActive) {
				triggerOffList.forEach(triggerOff => {
					triggerOff.targetElement = triggerElement;
					triggerOff.addEventListener("click", triggerOffHandler, false);
				});
			} else {
				triggerOffList.forEach(triggerOff => {
					triggerOff.removeEventListener("click", triggerOffHandler, false);
				});
			}
		}
	};

	/* Toggle class and aria on trigger and target elements. */
	const manageToggle = element => {
		const className = element.getAttribute(CLASS) || "is-active";
		element.isToggleActive = !element.isToggleActive;
		manageAria(element);

		if (!element.hasAttribute(TARGET_ONLY)) {
			element.classList.toggle(className);
		}

		const targetElements = retrieveTargets(element);
		for (let i = 0; i < targetElements.length; i++) {
			targetElements[i].classList.toggle(className);
			manageTarget(targetElements[i], element);
		}

		manageTriggerOutside(element);
	};

	/* Manage event ouside trigger or target elements. */
	const manageTriggerOutside = element => {
		if (element.hasAttribute(OUTSIDE)) {
			if (element.hasAttribute(GROUP)) {
				console.warn(`You can't use '${OUTSIDE}' on a grouped trigger`);
			} else {
				if (element.isToggleActive) {
					document.addEventListener(element.getAttribute(EVENT) || "click", documentEventHandler, false);
				} else {
					document.removeEventListener(element.getAttribute(EVENT) || "click", documentEventHandler, false);
				}
			}
		}
	};

	/* Toggle elements of a same group. */
	const manageGroup = element => {
		const groupActiveElements = retrieveGroupActiveElement(element.getAttribute(GROUP));

		if (groupActiveElements.length > 0) {
			if (groupActiveElements.indexOf(element) === -1) {
				groupActiveElements.forEach(manageToggle);
				manageToggle(element);
			}
		} else {
			manageToggle(element);
		}
	};

	/* Toggle elements set to be active by default. */
	const manageActiveByDefault = element => {
		const className = element.getAttribute(CLASS) || "is-active";
		element.isToggleActive = true;
		manageAria(element, {
			[CHECKED]: true,
			[EXPANDED]: true,
			[HIDDEN]: false,
			[SELECTED]: true
		});

		if (!element.hasAttribute(TARGET_ONLY) && !element.classList.contains(className)) {
			element.classList.add(className);
		}

		const targetElements = retrieveTargets(element);
		for (let i = 0; i < targetElements.length; i++) {
			if (!targetElements[i].classList.contains(className)) {
				targetElements[i].classList.add(className);
			}
			manageTarget(targetElements[i], element);
		}

		manageTriggerOutside(element);
	};

	/* Initialization. */
	const init = (() => {

		/* Active by default management. */
		$$(IS_ACTIVE).forEach(trigger => {
			if (trigger.hasAttribute(GROUP)) {
				const group = trigger.getAttribute(GROUP);
				if (retrieveGroupActiveElement(group).length > 0) {
					console.warn(`Toggle group '${group}' must not have more than one trigger with '${IS_ACTIVE}'`);
				} else {
					manageActiveByDefault(trigger);
				}
			} else {
				manageActiveByDefault(trigger);
			}
		});

		/* Set specified or click event on each trigger element. */
		$$().forEach(trigger => {
			trigger.addEventListener(trigger.getAttribute(EVENT) || "click", event => {
				event.preventDefault();
				(trigger.hasAttribute(GROUP) ? manageGroup : manageToggle)(trigger);
			}, false);
		});

		/* Escape key management. */
		const triggerEscElements = $$(ESCAPE);
		if (triggerEscElements.length > 0) {
			document.addEventListener("keyup", event => {
				event = event || window.event;
				if (event.key === "Escape" || event.key === "Esc") {
					triggerEscElements.forEach(trigger => {
						if (trigger.isToggleActive) {
							if (trigger.hasAttribute(GROUP)) {
								console.warn(`You can't use '${ESCAPE}' on a grouped trigger`);
							} else {
								manageToggle(trigger);
							}
						}
					});
				}
			}, false);
		}
	});

	/* eslint no-unused-vars: "off" */

	const onLoad = () => {
		init();
		document.removeEventListener("DOMContentLoaded", onLoad);
	};

	document.addEventListener("DOMContentLoaded", onLoad);
	window.initEasyToggleState = init;

}());
