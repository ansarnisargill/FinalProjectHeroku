using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectRenewed
{
    public class ChatSystem
    {
        public Dictionary<int, string[]> BDIQuesstions= new Dictionary<int, string[]>();
        public  Dictionary<int, string> chatQuestions = new Dictionary<int, string>();
        public Dictionary<int, string> Answers = new Dictionary<int, string>();
        public Dictionary<int, int> BDIAnswers = new Dictionary<int, int>();
        public ChatSystem()
        {
            chatQuestions.Add(0, "Hey! This is your DoctorBB! I am going to conduct your Depression Analysis Survey,<br> This would take only about 5 minutes! Are We ready to go?");
            chatQuestions.Add(1, "Ok! First Of all, tell me about your home environment?");
            chatQuestions.Add(2, "Whats your qualification?");
            chatQuestions.Add(3, "Ok! That was all for today!");

            BDIQuesstions.Add(0,new string[] { "Hello! This is BDI depression test. You need to choose one options from given four options! <br> You can reply with the number of option or color of that option!"});
            BDIQuesstions.Add(1, new string[] { "Whats your condition about \"Sadness\" ?", "I do not feel sad.", "I feel sad.", "I am sad all the time and I can't snap out of it.", "I am so sad and unhappy that I can't stand it." });
            BDIQuesstions.Add(2, new string[] { "what's your perspective about \"future\" ?", "I am not particularly discouraged about the future.", "I feel discouraged about the future.", "I feel I have nothing to look forward to.", "I feel the future is hopeless and that things cannot improve." });
            BDIQuesstions.Add(3, new string[] { "Do you consider yourself a \"Failure\" ?", "I do not feel like a failure.", "I feel I have failed more than the average person.", "As I look back on my life, all I can see is a lot of failures.","I feel I am a complete failure as a person." });
            BDIQuesstions.Add(4, new string[] { "Do you like to perform activities which you enjoyed performing in past?", "I get as much satisfaction out of things as I used to.","I don't enjoy things the way I used to.", "I don't get real satisfaction out of anything anymore.", "I am dissatisfied or bored with everything." });
            BDIQuesstions.Add(5, new string[] { "How much guilt do you endure?", "I don't feel particularly guilty.", "I feel guilty a good part of the time.", "I feel quite guilty most of the time.", "I feel guilty all of the time." });
            BDIQuesstions.Add(6, new string[] { "Do you feel like you are being punished for your actions in your life?", "I don't feel I am being punished.", "I feel I may be punished.", "I expect to be punished.", "I feel I am being punished." });
            BDIQuesstions.Add(7, new string[] { "Are you disappointed with yourself?", "I don't feel disappointed in myself.", "I am disappointed in myself.", "I am disgusted with myself.", "I hate myself." });
            BDIQuesstions.Add(8, new string[] { "How do you compare yourself with others?", "I don't feel I am any worse than anybody else.", "I am critical of myself for my weaknesses or mistakes.", "I blame myself all the time for my faults.","I blame myself for everything bad that happens." });
            BDIQuesstions.Add(9, new string[] { "Do you want to kill yourself?", "I don't have any thoughts of killing myself.", "I have thoughts of killing myself, but I would not carry them out.", "I would like to kill myself.", "I would kill myself if I had the chance." });
            BDIQuesstions.Add(10, new string[] { "Do you cry a lot?", "I don't cry any more than usual.", "I cry more now than I used to.", "I cry all the time now.", "I used to be able to cry, but now I can't cry even though I want to." });
            BDIQuesstions.Add(11, new string[] { "Do you get irritated easily?", "I am no more irritated by things than I ever was.", "I am slightly more irritated now than usual.", "I am quite annoyed or irritated a good deal of the time.", "I feel irritated all the time." });
            BDIQuesstions.Add(12, new string[] { "Do you like to meet other people?", "I have not lost interest in other people.", "I am less interested in other people than I used to be.", "I have lost most of my interest in other people.", "I have lost all of my interest in other people." });
            BDIQuesstions.Add(13, new string[] { "How reflexive is you decision making power?", "I make decisions about as well as I ever could.", "I put off making decisions more than I used to.", "I have greater difficulty in making decisions more than I used to.", "I can't make decisions at all anymore." });
            BDIQuesstions.Add(14, new string[] { "Do you feel unatteractive?", "I don't feel that I look any worse than I used to.", "I am worried that I am looking old or unattractive.","I feel there are permanent changes in my appearance that make me look unattractive", "I believe that I look ugly." });
            BDIQuesstions.Add(15, new string[] { "What about your job?", "I can work about as well as before.", "It takes an extra effort to get started at doing something.", "I have to push myself very hard to do anything.", "I can't do any work at all." });
            BDIQuesstions.Add(16, new string[] { "What is the condition of your sleep?", "I can sleep as well as usual.", "I don't sleep as well as I used to.", "I wake up 1-2 hours earlier than usual and find it hard to get back to sleep.", "I wake up several hours earlier than I used to and cannot get back to sleep." });
            BDIQuesstions.Add(17, new string[] { "Do you get tired easily?", "I don't get more tired than usual.", "I get tired more easily than I used to.", "I get tired from doing almost anything.", "I am too tired to do anything." });
            BDIQuesstions.Add(18, new string[] { "What about your appetite?", "My appetite is no worse than usual.", "My appetite is not as good as it used to be.", "My appetite is much worse now.", "I have no appetite at all anymore." });
            BDIQuesstions.Add(19, new string[] { "Have you lost any weight?", "I haven't lost much weight, if any, lately.", "I have lost more than five pounds.", "I have lost more than ten pounds.", "I have lost more than fifteen pounds." });
            BDIQuesstions.Add(20, new string[] { "Are you worried about your health?", "I am no more worried about my health than usual.","I am worried about physical problems like aches, pains, upset stomach, or constipation.", "I am very worried about physical problems and it's hard to think of much else.", "I am so worried about my physical problems that I cannot think of anything else." });
            BDIQuesstions.Add(21, new string[] { "What about your \"Libido\"", "I have not noticed any recent change in my interest in sex.", "I am less interested in sex than I used to be.", "I have almost no interest in sex.", "I have lost interest in sex completely." });



        }
    }
}