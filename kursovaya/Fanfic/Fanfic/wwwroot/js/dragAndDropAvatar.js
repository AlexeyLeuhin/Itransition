$(function () {
    $("#divUploadFile").filedrop({
        fallback_id: 'update-profile-button',
        fallback_dropzoneClick: false,
        url: '/FileUpload/UploadAvatar',
        allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif'],
        allowedfileextensions: ['.jpg', '.jpeg', '.png', '.gif'],
        paramname: 'avatar',
        maxfiles: 1,
        maxfilesize: 5,
        dragOver: function () {
            document.getElementById("divUploadFile").classList.add('drop-place-active');
        },
        dragLeave: function () {
            $("#divUploadFile").removeClass('drop-place-active');
        },
        drop: function () {
            $("#imgLoading").show();
        },
        beforeEach: function (file) {
            new Compressor(file, {
                quality: 0.6,
                width: 600,
                height: 600,
                success(result) {
                    const formData = new FormData();
                    formData.append('avatar', result, result.name);
                    $.ajax({
                        url: "/FileUpload/UploadAvatar",
                        data: formData,
                        processData: false,
                        contentType: false,
                        type: 'Post',
                        success: function (response) {
                            $("#divUploadFile").removeClass('drop-place-active');
                            $("#imgLoading").hide();
                            location.reload();
                        }
                    }); 
                }
            })
            return false;
        }
    });    
    
});

