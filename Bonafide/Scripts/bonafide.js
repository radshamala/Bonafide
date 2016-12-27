function isNumberKey(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode > 31 && (charCode < 48 || charCode > 57))

        return false;

    return true;

}


$(".feesjs").keypress(function () {
    var me = $(this);
    if (me.val() == "0")
        me.val("");

});



