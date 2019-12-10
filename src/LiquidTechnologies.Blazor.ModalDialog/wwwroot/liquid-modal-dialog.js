
window.liquidModalDialog = {
    showPrompt: function (message, defaultValue) {
        return prompt(message, defaultValue);
    },
    focusElement: function (element) {
        element.focus();
    }
};
