$(document).ready(function () {
    
    
    
    
    //*************************************************************************************
    //
    // Function to sync table header and footer column widths to main table column widths
    //
    //*************************************************************************************

    var syncColumnWidths = function () {
        $('[id^="numbers-table"]').each(function () {
            $(this).find("> tbody > tr > td").each(function (i) {
                $(this).width($($("#add-pick-table-noauth tr td")[i]).width());
            });
        });
    };

    //**********************************
    //
    // Function to position error areas
    //
    //***********************************

    var positionErrorMessages = function () {
        $(".error-anchor-bottom").each(function () {
            var offset = $(this).closest("td").find("input").position();
            var top = offset.top + 44;
            var left = offset.left - 16;
            $(this).css({ top: top, left: left, position: "absolute" }).width(20);
        });
        $(".error-anchor-top").each(function () {
            var offset = $(this).closest("td").find("input").position();
            var top = offset.top - 34;
            var left = offset.left - 10;
            $(this).css({ top: top, left: left, position: "absolute" });
        });
    };

    //*****************************
    //
    // Error notification Function
    //
    //*****************************
    var showErrorNotification = function (row, message) {
        $(row).find("*").not(".btn").not("span").each(function () {
            $(this).addClass("errorHighlighted");
        });

        setTimeout(function () {
            $(row).find("*").not(".btn").not("span").each(function () {
                $(this).removeClass("errorHighlighted");
            });
        }, 250);

        toastr.error(message);
    };

    //*****************************
    //
    // Add a pick row
    //
    //*****************************
    $("#button-add").on("click", function (event) {
        event.preventDefault();

        var currentForm = $(this).closest("form");
        var isValid = $(currentForm).validate().form();
        if (!isValid) return false;

        //if (userAuthorized)
        if (checkType == "set")
            var row = $("#button-add").closest("tr");
        else
            var row = $("#add-row");

        var megaMillionPick = {
            FirstPick: row.find("input:eq(0)").val(),
            SecondPick: row.find("input:eq(1)").val(),
            ThirdPick: row.find("input:eq(2)").val(),
            FourthPick: row.find("input:eq(3)").val(),
            FifthPick: row.find("input:eq(4)").val(),
            MegaPick: row.find("input:eq(5)").val()
        };

        var jqXhr = app.ajax({
            url: window.createAction + "?checkType=" + GetQueryStringParams("checkType"),
            context: this,
            contentType: "application/json; charset=utf-8",
            type: "POST",
            datatype: "json",
            data: JSON.stringify(megaMillionPick),
            error: function () {
                if (jqXhr.status === 409) {
                    showErrorNotification(row, "Pick already exists, pick not added.");
                } else {
                    showErrorNotification(row, "Could not add this pick.");
                }
            },
            success: function (data) {
                if (jqXhr.status === 400) {
                    showErrorNotification(row, "Could not add this pick.");
                } else {

                    // Sucessful insert
                    var number1 = row.find("input:eq(0)").val();
                    var number2 = row.find("input:eq(1)").val();
                    var number3 = row.find("input:eq(2)").val();
                    var number4 = row.find("input:eq(3)").val();
                    var number5 = row.find("input:eq(4)").val();
                    var megaNumber = row.find("input:eq(5)").val();
                    var newRowCount = $(".wrapper form").length + 1;

                    if (checkType == "set") {
                        var clonedFormNew = clonedForm.clone(true);
                        clonedFormNew.prop("id", "mega-millions-form" + newRowCount);
                        var clonedRow = $(clonedFormNew, "table", "tbody", "tr");

                        clonedFormNew.find("tr input").each(function () {
                            var newName = $(this).prop("name") + newRowCount;
                            $(this).prop("name", newName);
                            var newId = $(this).prop("id") + newRowCount;
                            $(this).prop("id", newId);
                        });
                        clonedRow.find("button[data-id]").each(function () {
                            $(this).attr("data-id", data.Id);
                        });
                        clonedRow.find("span[data-valmsg-for]").each(function (i) {
                            if (i === 0)
                                $(this).attr("data-valmsg-for", "FirstPick" + newRowCount);
                            if (i === 1)
                                $(this).attr("data-valmsg-for", "second-pick" + newRowCount);
                            if (i === 2)
                                $(this).attr("data-valmsg-for", "third-pick" + newRowCount);
                            if (i === 3)
                                $(this).attr("data-valmsg-for", "fourth-pick" + newRowCount);
                            if (i === 4)
                                $(this).attr("data-valmsg-for", "fifth-pick" + newRowCount);
                            if (i === 5)
                                $(this).attr("data-valmsg-for", "mega-pick" + newRowCount);
                        });

                        clonedRow.find("input:eq(1)").val(number1);
                        clonedRow.find("input:eq(2)").val(number2);
                        clonedRow.find("input:eq(3)").val(number3);
                        clonedRow.find("input:eq(4)").val(number4);
                        clonedRow.find("input:eq(5)").val(number5);
                        clonedRow.find("input:eq(6)").val(megaNumber);
                        $(clonedFormNew).insertBefore("#form-insert-marker");

                        var table = $("#numbers-table");
                        table.prop("id", table.prop("id") + newRowCount);
                        var tableRow = $("#table-row");
                        table - row.prop("id", table - row.prop("id") + newRowCount);
                        clonedFormNew.removeData("validator")
                            .removeData("unobtrusiveValidation");
                        $.validator.unobtrusive.parse(clonedFormNew);
                        clonedFormNew.validate();

                        $("#second-pick" + newRowCount).rules("add", {
                            checkNumber2: true,
                            messages: {
                                checkNumber2: "Duplicate"
                            }
                        });
                        $("#third-pick" + newRowCount).rules("add", {
                            checkNumber3: true,
                            messages: {
                                checkNumber3: "Duplicate"
                            }
                        });
                        $("#fourth-pick" + newRowCount).rules("add", {
                            checkNumber4: true,
                            messages: {
                                checkNumber4: "Duplicate"
                            }
                        });
                        $("#fifth-pick" + newRowCount).rules("add", {
                            checkNumber5: true,
                            messages: {
                                checkNumber5: "Duplicate"
                            }
                        });

                        // toggle UI buttons
                        $(".button-update").hide();
                        $(".button-cancel").hide();

                        // clear the entry row
                        $(row).find("input").val("");

                        // reset the add row
                        $("#mega-millions-add-form").validate().resetForm();

                        syncColumnWidths();

                        toastr.success("Pick added.");

                        // focus on first number in the add pick row
                        //$("#first-pick-add").select();
                    }
                }
                positionErrorMessages();
                window.getWinningDraws();

                // let the user know something happened
                row.find("*").not(".btn").not("span").each(function () {
                    $(this).addClass("highlighted");
                });

                setTimeout(function () {
                    row.find("*").not(".btn").not("span").each(function () {
                        $(this).removeClass("highlighted");
                    });
                }, 150);

                window.getWinningDraws();
            }
        });
        return false;
    });

    //*****************************
    //
    // Clear the add pick row
    //
    //*****************************

    $("#button-clear").on("click", function (event) {
        event.preventDefault();

        //clear validation error ballons
        $(".field-validation-error").addClass("field-validation-valid");
        $(".field-validation-error").removeClass("field-validation-error");

        //clear the text boxes
        if (checkType == "set")
            var row = $("#button-add").closest("tr");
        else
            var row = $("#add-row");

        row.find("input").val("");

        // reset the add row
        $("#mega-millions-add-form").validate().resetForm();

        // focus on first input in add row
        //$("#first-pick-add").select();

    });

    //*****************************
    //
    // Update a pick row
    //
    //*****************************

    function GetQueryStringParams(sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }
    
    $(document).on("click", ".button-update", function (event) {
        event.preventDefault();

        var row = $(this).closest("tr");

        //var pickNumber = row.children(":first").text();
        var currentForm = $(this).closest("form");
        var isValid = $(currentForm).validate().form();

        if (!isValid) {
            return false;
        }

        $(".field-validation-error").addClass("field-validation-valid");
        $(".field-validation-error").removeClass("field-validation-error");

        var megaMillionPick = {
            id: $(this).attr("data-id"),
            FirstPick: row.find("input:eq(0)").val(),
            SecondPick: row.find("input:eq(1)").val(),
            ThirdPick: row.find("input:eq(2)").val(),
            FourthPick: row.find("input:eq(3)").val(),
            FifthPick: row.find("input:eq(4)").val(),
            MegaPick: row.find("input:eq(5)").val()
        };

        var jqXhr = app.ajax({
            url: window.updateAction + "?checkType=" + GetQueryStringParams("checkType"),
            context: this,
            contentType: "application/json; charset=utf-8",
            type: "PUT",
            datatype: "json",
            data: JSON.stringify(megaMillionPick),
            error: function () {
                if (jqXhr.status === 409) {
                    showErrorNotification(row, "Pick already exists, pick not updated.");
                } else {
                    showErrorNotification(row, "Pick could not be updated.");
                }
            },
            success: function () {
                if (jqXhr.status === 304) {
                    showErrorNotification(row, "Pick could not be updated.");
                } else {
                    // Sucessful update
                    // toggle the UI buttons
                    $(this).hide();
                    $(".button-delete").show();
                    $(".button-edit").show();
                    $(this).nextAll(".button-cancel:first").hide();

                    // clear validation errors
                    $(".lotto-box").removeClass("input-validation-error");

                    // remove the border around and disable text boxes on the update row
                    row.find("input").removeClass("lotto-box").addClass("lotto-text").attr("readonly", true);

                    // let the user know the row was updated
                    row.find("*").not(".btn").not("span").each(function () {
                        $(this).addClass("highlighted");
                    });

                    setTimeout(function () {
                        row.find("*").not(".btn").not("span").each(function () {
                            $(this).removeClass("highlighted");
                        });
                        // expose the edit boxes on add row and focus on the first input box
                        $("#button-add").attr("disabled", false);
                        $("#button-clear").attr("disabled", false);
                        var addRow = $("#button-add").closest("tr");
                        addRow.find("input").attr("disabled", false).addClass("lotto-box").removeClass("lotto-box-disabled");
                        addRow.find("input").css("cursor", "auto");
                        //addRow.find("input:eq(0)").select();

                        $(".field-validation-error").addClass("field-validation-valid");
                        $(".field-validation-error").removeClass("field-validation-error");
                        // focus on first number in the add pick row
                        //$("#first-pick-add").select();
                    }, 250);

                    toastr.success( "Pick saved.");

                    window.getWinningDraws();
                    editMode = "Add";
                }
            }
        });
        return false;
    });

    //*****************************
    //
    // Delete a pick row
    //
    //*****************************

    $(document).on("click", ".button-delete", function (event) {
        event.preventDefault();
        var deleteButton = $(this);
        var row = deleteButton.closest("tr");
        var form = deleteButton.closest("form");
        //var pickNumber = $(row).closest("tr").children(":first").text();
        bootbox.confirm("Are you sure you want to delete this pick?", function (result) {
            if (result) {
                $(".field-validation-error > span").each(function () {
                    $(this).html("");
                });

                $("#mega-millions-add-form").validate().resetForm();

                var param = {
                    id: deleteButton.attr("data-id")
                };

                $.ajax({
                    url: window.deleteAction + "?checkType=" + GetQueryStringParams("checkType"),
                    context: this,
                    contentType: "application/x-www-form-urlencoded",
                    type: "DELETE",
                    datatype: "json",
                    data: param,
                    error: function () {
                        showErrorNotification(row, "Pick could not be deleted.");
                    },
                    success: function () {
                        row.find("*").not(".btn").not("span").each(function () {
                            $(this).addClass("highlighted");
                        }
                        );

                        setTimeout(function () {
                            form.remove(); // remove the pick form
                            //$($('[id^="table-row"]')).each(function (i) { // re-number the picks
                            //    $(this).children(":first").html("Pick&nbsp" + (i + 1) + ":");
                            //});
                            window.getWinningDraws();
                            $(".field-validation-error").addClass("field-validation-valid");
                            $(".field-validation-error").removeClass("field-validation-error");
                        }, 250);

                        toastr.success("Pick deleted.");
                    }
                });
            }

            //setTimeout(function () {
            //$("#first-pick-add").select();
            //}, 250);
        });
    });

    //*****************************
    //
    // Edit a pick row
    //
    //*****************************

    $(document).on("click", ".button-edit", function (event) {
        event.preventDefault();

        editMode = "Edit";

        $(".field-validation-error").addClass("field-validation-valid");
        $(".field-validation-error").removeClass("field-validation-error");

        var row = $(this).closest("tr");
        //save the current values for the row in case the user cancels the edit
        saveFirstNumber = row.find("input:eq(0)").val();
        saveSecondNumber = row.find("input:eq(1)").val();
        saveThirdNumber = row.find("input:eq(2)").val();
        saveFourthNumber = row.find("input:eq(3)").val();
        saveFifthNumber = row.find("input:eq(4)").val();
        saveMegaNumber = row.find("input:eq(5)").val();

        //toggle the buttons on the UI
        $(this).hide();
        $(".button-delete").hide();
        $(".button-edit").hide();
        $(this).nextAll(".button-cancel").show();
        $(this).nextAll(".button-update").show();

        // clear any validation errors from the add row
        $(".field-validation-error > span").each(function () {
            $(this).html("");
        });

        // reset the add row
        $("#mega-millions-add-form").validate().resetForm();

        //clear the add row and remove the border around text boxes
        var addRow = $("#button-add").closest("tr");
        $("#button-add").attr("disabled", true);
        $("#button-clear").attr("disabled", true);
        addRow.find("input").val("").removeClass("lotto-box").addClass("lotto-box-disabled").attr("disabled", true);
        addRow.find("input").css("cursor", "not-allowed");

        //expose the edit boxes on the selected row and focus on the first input box
        row = $(this).closest("tr");
        row.find("input").addClass("lotto-box").removeClass("lotto-text").attr("readonly", false);
        //row.find("input:eq(0)").select();
    });

    //*****************************
    //
    // Cancel Update
    //
    //*****************************

    $(document).on("click", ".button-cancel", function (event) {
        event.preventDefault();
        editMode = "Add";
        $(".field-validation-error").addClass("field-validation-valid");
        $(".field-validation-error").removeClass("field-validation-error");

        // clear validation errors from edit
        $(".field-validation-error > span").each(function () {
            $(this).html("");
        });

        // toggle the UI buttons
        $(".button-update").hide();
        $(this).hide();
        $(".button-delete").show();
        $(".button-edit").show();
        $("#add-pick-row").attr("disabled", false);
        $(this).nextAll(".button-cancel:first").hide();

        // clear validation errors
        $(".lotto-box").removeClass("input-validation-error");

        var row = $(this).closest("tr");
        row.find("input:eq(0)").val(saveFirstNumber);
        row.find("input:eq(1)").val(saveSecondNumber);
        row.find("input:eq(2)").val(saveThirdNumber);
        row.find("input:eq(3)").val(saveFourthNumber);
        row.find("input:eq(4)").val(saveFifthNumber);
        row.find("input:eq(5)").val(saveMegaNumber);

        // remove the border around and disable text boxes on the update row
        row.find("input").removeClass("lotto-box").addClass("lotto-text").attr("readonly", true);;

        // expose the edit boxes on add row and focus on the first input box
        $("#button-add").attr("disabled", false);
        $("#button-clear").attr("disabled", false);
        var addRow = $("#button-add").closest("tr");
        addRow.find("input").attr("disabled", false).addClass("lotto-box").removeClass("lotto-box-disabled");
        addRow.find("input").css("cursor", "auto");
        //addRow.find("input:eq(0)").select();
    });


    //********************************
    //
    // Create custom validation rules
    //
    //********************************

    var createValidationRules = function () {
        // Add custom validator methods
        $.validator.addMethod("checkNumber2",
            function (value, element) {
                var result = false;
                var row = $(element).closest("tr");
                var number1 = parseInt(row.find("input:eq(0)").val());
                var number2 = parseInt(row.find("input:eq(1)").val());
                if (number1 !== number2) {
                    result = true;
                }
                return result;
            });

        $.validator.addMethod("checkNumber3",
            function (value, element) {
                var result = false;
                var row = $(element).closest("tr");
                var number1 = parseInt(row.find("input:eq(0)").val());
                var number2 = parseInt(row.find("input:eq(1)").val());
                var number3 = parseInt(row.find("input:eq(2)").val());
                if (number3 !== number1 && number3 !== number2) {
                    result = true;
                }
                return result;
            });

        $.validator.addMethod("checkNumber4",
            function (value, element) {
                var result = false;
                var row = $(element).closest("tr");
                var number1 = parseInt(row.find("input:eq(0)").val());
                var number2 = parseInt(row.find("input:eq(1)").val());
                var number3 = parseInt(row.find("input:eq(2)").val());
                var number4 = parseInt(row.find("input:eq(3)").val());
                if (number4 !== number1 && number4 !== number2 && number4 !== number3) {
                    result = true;
                }
                return result;
            });

        $.validator.addMethod("checkNumber5",
            function (value, element) {
                var result = false;
                var row = $(element).closest("tr");
                var number1 = parseInt(row.find("input:eq(0)").val());
                var number2 = parseInt(row.find("input:eq(1)").val());
                var number3 = parseInt(row.find("input:eq(2)").val());
                var number4 = parseInt(row.find("input:eq(3)").val());
                var number5 = parseInt(row.find("input:eq(4)").val());
                if (number5 !== number1 && number5 !== number2 && number5 !== number3 && number5 !== number4) {
                    result = true;
                }
                return result;
            });


        $('[name*="second-pick"]').each(function () {
            $(this).rules("add", {
                checkNumber2: true,
                messages: {
                    checkNumber2: "Duplicate"
                }
            });
        });

        $('[name*="third-pick"]').each(function () {
            $(this).rules("add", {
                checkNumber3: true,
                messages: {
                    checkNumber3: "Duplicate"
                }
            });
        });

        $('[name*="fourth-pick"]').each(function () {
            $(this).rules("add", {
                checkNumber4: true,
                messages: {
                    checkNumber4: "Duplicate"
                }
            });
        });

        $('[name*="fifth-pick"]').each(function () {
            $(this).rules("add", {
                checkNumber5: true,
                messages: {
                    checkNumber5: "Duplicate"
                }
            });
        });
    };

    //***********************************************
    //
    // Only allow integer entry into input fields
    //
    //***********************************************

    $("input[type=tel]").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, f5, f12 and enter
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 116, 123 ]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }

        // Ensure that it is a number and stop the keypress
        var editBox = $(this);
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105) || (e.keyCode === 48 && editBox.val() === "")) {
            editBox.addClass("highlight-pink");
            e.preventDefault();
            setTimeout(function () {
                editBox.removeClass("highlight-pink");
            }, 250);
        }
    });

    //************************************************************
    //
    // Select the entire number in the textbox on a single click
    //
    //************************************************************

    $("[id^='numbers-table']").on("click", ".lotto-box", function () {
        $(this).select();
    });

    $("#add-pick-table").on("click", ".lotto-box", function () {
        $(this).select();
    });

    $("#add-pick-table-noauth").on("click", ".lotto-box", function () {
        $(this).select();
    });

    //************************************************************
    //
    // handle ajax animation
    //
    //************************************************************

    $(document)
        .ajaxStart(function () {
            $("#logoImage").css({ top: -100, left: -100, position: "absolute" });
            $(":button").prop("disabled", true);
            $("#ajax-spinner-container").show();
        })
        .ajaxStop(function () {
            $("#ajax-spinner-container").hide();
            $("#logoImage").css({ top: 0, left: 0, position: "absolute" });
            $(":button").prop("disabled", false);
            if (editMode === "Edit") {
                $("#button-add").attr("disabled", true);
                $("#button-clear").attr("disabled", true);
            }
        });

    //************************************************************
    //
    // Reposition error messages when browser is resized
    //
    //************************************************************

    $(window).resize(function () {
        syncColumnWidths();
        positionErrorMessages();
    });

    //*****************************
    //
    // Get the winning draws
    //
    //*****************************

    window.getWinningDraws = function () {

        //if (userAuthorized) {
        if (checkType == "set") {
            $.ajax({
                type: "GET",
                url: window.getWinningNumbersAction + "?checkType=" + GetQueryStringParams("checkType"),
                success: function (data) {

                    $("#winning-draws").html(data);
                    // let the user know something happened
                    $("#draws-heading").find("*").not(".btn").addClass("highlight-yellow");
                    setTimeout(function () {
                        $("#draws-heading").find("*").not(".btn").removeClass("highlight-yellow");
                    }, 150);
                },
                error: function (data) {
                    alert(data.errorText);
                }
            });
        }
        else
        {
            $.ajax({
                type: "GET",
                url: window.getWinningNumbersActionAnonymous + "?checkType=" + GetQueryStringParams("checkType"),
                success: function (data) {
                    $("#winning-draws").html(data);
                    // let the user know something happened
                    $("#winning-draws").find("*").not(".btn").addClass("highlight-yellow");
                    setTimeout(function () {
                        $("#winning-draws").find("*").not(".btn").removeClass("highlight-yellow");
                    }, 150);
                },
                error: function (data) {
                    alert(data.errorText);
                }
            });
        }
    };

    //**********************************
    //
    // Open OData in new browser window
    //
    //**********************************

    $("#odata-link").on("click", function (event) {
        event.preventDefault();
        window.open(odataAction, "oddata-window", "width=1350,height=800");
        //$(':input:enabled:visible:first').focus();
    });

    
    //------------------------------------------------------------------    
    //---------------------- Main
    //------------------------------------------------------------------    

    // hide buttons not needed when page is first displayed
    $(".button-update").hide();
    $(".button-cancel").hide();

    $(".lotto-text").each(function () {
        $(this).attr("readonly", true);
    });

    // create custom validation rules
    createValidationRules();

    // display winning draws for this user
    window.getWinningDraws();

    // sync the number table column widths to the add table column widths
    $(window).load(function () {
        syncColumnWidths();
        positionErrorMessages();
    });


    // save the dummy form to a variable to use as a form when creating a new row, and remove the dummy form from the DOM
    var form = $("#mega-millions-form");
    var clonedForm = form.clone(true);
    form.remove();

    // global variables to save the row in case it needs to be reset
    var saveFirstNumber;
    var saveSecondNumber;
    var saveThirdNumber;
    var saveFourthNumber;
    var saveFifthNumber;
    var saveMegaNumber;
    var editMode = "Add";

    $("#elmah-log-link").on("click", function (event) {
        event.preventDefault();
        window.open(elmahAction, "elmah-log-window", "width=1350,height=800");
    });
       



    $("#btn-quick-check-not-logged-in").on("click", function ()
    {
        window.location = indexActionQuick;
    });

    $("#btn-quick-check-logged-in").on("click", function () {
        window.location = indexActionQuick;
    });

    $("#btn-quick-check-landscape-not-logged-in").on("click", function () {
        window.location = indexActionQuick;
    });

    $("#btn-quick-check-landscape-logged-in").on("click", function () {
        window.location = indexActionQuick;
    });





    $("#btn-saved-picks").on("click", function ()
    {
        window.location = indexActionSet;
    });

    var checkOrientation = function (e) {
        window.setTimeout(function () { // a little delay so slow devices can set the window width after reorientation.
            var windowWidth = $(window).width();
            //alert("window200x width:" +windowWidth);
            if (windowWidth < 480) {
                $("#portrait-container").show();
                $("#landscape-container").hide();
            }
            else {
                $("#portrait-container").hide();
                $("#landscape-container").show();
            }
            positionErrorMessages();
        }, 200);
    }

    checkOrientation();

    $(window).on("orientationchange", function () {
        checkOrientation();
    });

        
    // focus on first number in the add pick row
    //$("#first-pick-add").select();

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "0",
        "extendedTimeOut": "0",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    var showUserAgreement = function () {
        $.ajax({
            url: window.userAgreementUrl,
            success: function (result) {
                bootbox.dialog({
                    closeButton: false,
                    message: result,
                    title: "User Agreement",
                    buttons: {
                        main: {
                            label: "I agree to the user agreement",
                            className: "btn-primary",
                            callback: function () {
                                $.ajax({
                                    type: "GET",
                                    url: window.userAgreementFlagAction,
                                });
                            }
                        }
                    }
                })
                    //.find('.messgeContainer').height((getViewableHeight() / 2) - 80).css('overflow', 'auto');;
            }
        });
    };
    
    //function getViewableHeight() {
    //    var myWidth = 0, myHeight = 0;
    //    if (typeof (window.innerWidth) == 'number') {
    //        //Non-IE
    //        myWidth = window.innerWidth;
    //        myHeight = window.innerHeight;
    //    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
    //        //IE 6+ in 'standards compliant mode'
    //        myWidth = document.documentElement.clientWidth;
    //        myHeight = document.documentElement.clientHeight;
    //    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
    //        //IE 4 compatible
    //        myWidth = document.body.clientWidth;
    //        myHeight = document.body.clientHeight;
    //    }
    //    //   window.alert('Width = ' + myWidth);
    //    // window.alert('Height = ' + myHeight);

    //    return myHeight;
    //}

    if (!userAuthorized) {
        if (userAgreementMessage != "") {
            showUserAgreement();
        }
    }

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-bottom-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "400",
        "timeOut": "1500",
        "extendedTimeOut": "1500",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $("#user-agreement-link").on("click", function () {
        showUserAgreement();
    });

    $.fn.animateRotate = function (angle, duration, easing, complete) {
        return this.each(function () {
            var $elem = $(this);

            $({ deg: 0 }).animate({ deg: angle }, {
                duration: duration,
                easing: easing,
                step: function (now) {
                    $elem.css({
                        transform: 'rotate(' + now + 'deg)'
                    });
                },
                complete: complete || $.noop
            });
        });
    };

    window.setTimeout(function () {
        $('#rotate-me').animateRotate(360,1200);
    }, 50);

    //$("#firstPick").focus();
    //$('input:text:enabled:first').focus();

    //document.getElementById("first-pick-add").focus();
    
});



