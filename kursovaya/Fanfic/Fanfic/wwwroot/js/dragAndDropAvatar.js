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
            $("#divUploadFile").addClass('');
        },
        dragLeave: function () {
            $("#divUploadFile").addClass('');
        },
        drop: function () {
            $("#imgLoading").show();
        },
        afterAll: function () {
            $("#divUploadFile").addClass('');
        },
        afterAll: function () {
            $("#imgLoading").hide();
            location.reload();
        }
    });
});

