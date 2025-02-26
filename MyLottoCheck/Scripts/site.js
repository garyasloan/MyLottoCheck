
//user agreement
var termsAgree = function (contentArea) {
    var body = $("body");
    body.css("height", "auto").css("height", body.height()); // Prevent page flicker on slideup
    if ($(contentArea).html() === "") {
        $.ajax({
            url: window.userAgreementUrl,
            success: function(result) {
                $(contentArea).html(result);
            }
        });
    }
    $(contentArea).slideToggle();
    return false;
}

    $("#terms").on("click", function (event) {
        event.preventDefault();
        termsAgree("#content-area");
    });

    jQuery.validator.unobtrusive.adapters.add("mandatory", function (options) {
        if (options.element.tagName.toUpperCase() === "INPUT" && options.element.type.toUpperCase() === "CHECKBOX") {
            options.rules["required"] = true;
            if (options.message)
                options.messages["required"] = options.message;
        }
    });

    
    $(function () {
        // menu handler
        var url = window.location.pathname;
        var activePage = url.substring(url.lastIndexOf('/') + 1);
        $('.nav li a').each(function () {
            var currentPage = this.href.substring(this.href.lastIndexOf('/') + 1);
            if (activePage == currentPage) {
                $(this).parent().addClass('active');
            }
        });

        // make the whole logo a link
        $("#logo").click(function () {
            window.location = $(this).find("a").attr("href");
            return false;
        });

        // elmah link
        $("#elmah-log-link").on("click", function (event) {
            event.preventDefault();
            window.open(elmahAction, "elmah-log-window", "width=1350,height=800");
            $(':input:enabled:visible:first').focus();
        });
    });

    
$(function () {
    //document.getElementById("first-pick-add").focus();
    setTimeout(function () { document.getElementById("first-pick-add").focus(); }, 250);
    //setTimeout(function () { $('input[name="q"]').focus() }, 3000);

    //$('.lotto-box :input:first').focus();
});


    


    //=============================================================
    // Common app utilites for ajax
    // http://blog.rohit-lakhanpal.info/2013/10/validating-anti-forgery-tokens-over.html
    //=============================================================
    (function (app, $) {
        app.getAntiForgeryToken = function () {
            return $('[name=__RequestVerificationToken]').val();
        };

        // Create a custom ajax function
        app.ajax = function (options) {
            // (a): Add the anti forgery tokens
            options.headers = options.headers || {};
            options.headers.__RequestVerificationToken = app.getAntiForgeryToken();

            // (b): Make the ajax call
            var jqXhr = $.ajax(options);
            return jqXhr;
        };

    })(this.app = this.app || {}, jQuery);

    