
document.write('<script src="/_catalogs/masterpage/AIC/js/jquery-3.5.1.min.js"><\/script>');
document.write('<script src="/_catalogs/masterpage/AIC/js/jquery.SPServices.min.js"><\/script>');

function runAfterEverythingElse() {
    var placeholderField = $('[id^=AICEmailPlaceholders][id$=TextField]');
    if (placeholderField.val() != '')
        placeholderField.prop("readonly", true);
}
_spBodyOnLoadFunctionNames.push("runAfterEverythingElse");