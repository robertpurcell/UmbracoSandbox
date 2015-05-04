$(function () {
    $('#contact-form').submit(function () {
        var thisForm = $(this);
        if (thisForm.valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    $('#form-container').html(result);
                    thisForm[0].reset();
                }
            });
        }
        return false;
    });
});
