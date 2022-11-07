/// <reference path="JQuery/jquery.3.2-vsdoc2.js" />

$(document).ready(function() {
    $('#divValidationSummary').draggable({ cursor: 'move' });
});

// Override of an ASP.NET Validation Method
var isFirstValidation = true;
var originalWidths = new Array();
function Page_ClientValidate(validationGroup) {
    Page_InvalidControlToBeFocused = null;
    Page_ValidationSummaries[0].validationGroup = validationGroup;
        if (typeof (Page_Validators) == "undefined") {
            return true;
        }
        var fieldsToValidate = new Array();
        var i;
        for (i = 0; i < Page_Validators.length; i++) {
            ValidatorValidate(Page_Validators[i], validationGroup, null);
            fieldsToValidate[i] = Page_Validators[i];
        }
        ValidatorUpdateIsValid();
        ValidationSummaryOnSubmit(validationGroup);
        Page_BlockSubmit = !Page_IsValid;
        
        if (!Page_IsValid) {
            for (i = 0; i < fieldsToValidate.length; i++) {

                if (!fieldsToValidate[i].isvalid) {
                    originalWidths[i] = $("#" + fieldsToValidate[i].controltovalidate).width();
                    $("#" + fieldsToValidate[i].controltovalidate).addClass('requiredFieldStyle');
                    
//                    if (isFirstValidation) {
//                        $("#" + fieldsToValidate[i].controltovalidate).width($("#" + fieldsToValidate[i].controltovalidate).width() - 4);
//                    }
                }
                else {
                    $("#" + fieldsToValidate[i].controltovalidate).removeClass("requiredFieldStyle");
//                    $("#" + fieldsToValidate[i].controltovalidate).width(originalWidths[i]);
                }
            }

            if (isFirstValidation) {
                isFirstValidation = false;
            }

            $("#divValidationSummary").attr("style", "display: block;");
            $("#divValidationSummary").attr("style", "z-index: 9999;");
        }
        
        return Page_IsValid;
    }
