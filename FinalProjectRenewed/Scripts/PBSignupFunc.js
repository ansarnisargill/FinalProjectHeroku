    $('#example').photobooth().on("image", function (event, dataUrl) {

        $("#gallery").append('<img src="' + dataUrl + '" >');
        saveImage(dataUrl);

    });
    function saveImage(img) {
        var id = $('#idval').val();
        var block = img.split(";");
        // Get the content type of the image
        var contentType = block[0].split(":")[1];// In this case "image/gif"
        // get the real base64 content of the file
        var realData = block[1].split(",")[1];// In this case "R0lGODlhPQBEAPeoAJosM...."
        // put your keys in the header
        var headers = {
            "Content-type": "application/json",
            "app_id": "9710ce6c",
            "app_key": "5415b25df0cb7c593f63c53306c59885"
        };
        //var payload = { "image": imageData, "gallery_name": "myGallery", "subject_id": "mySubjectID" };
        var payload = {
            "image": realData, "gallery_name": "Clients", "subject_id": id
        };
        var url = "http://api.kairos.com/enroll";
        $.ajax(url, {
            headers: headers,
            type: "POST",
            data: JSON.stringify(payload),
            dataType: "text"
        }).done(function (response) {
            var data = JSON.parse(response);
            if (data.face_id != null) {
                alert("Your Face ID has been set!");
            }
            else {
                alert("Sorry! Your Face Id has not been set!")
            }
        });

    };