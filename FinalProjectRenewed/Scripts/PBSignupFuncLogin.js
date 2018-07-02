    $('#example').photobooth().on("image", function (event, dataUrl) {

        
        saveImage(dataUrl);

    });
    function saveImage(img) {
        $("#Loader").show();
        $("example").hide();
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
            "image": realData, "gallery_name": "Clients"
        };
        var url = "http://api.kairos.com/recognize";
        $.ajax(url, {
            headers: headers,
            type: "POST",
            data: JSON.stringify(payload),
            dataType: "text"
        }).done(function (response) {
            console.log(JSON.parse(response));
                var data = JSON.parse(response);
                if (data.images != null && data.images.candidates != null) {
                    window.location = "http://localhost:52673/Home/LoginByPic/"+ data.images.candidates.subject_id;
            }
            else {
                    alert("Your face can not be recognized, take a better photo or login with email");
                    $("#Loader").hide();
                    $("example").show();

                }

        });

    };