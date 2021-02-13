$(document).ready(function () {

    $("#checkAll").click(function () {
        $(".checkBox").prop('checked',
            $(this).prop('checked'));
    });
});
