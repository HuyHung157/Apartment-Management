$(document).ready(function () {
    $("#btnShowChangePasswordModal").click(function () {
        $("#changePasswordModal").modal('show');
    });

    $("#btnHideChangePasswordModal").click(function (e) {
        $("#changePasswordModal").modal('hide');
    });
});