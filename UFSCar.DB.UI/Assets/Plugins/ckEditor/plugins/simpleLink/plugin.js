var elements = [];
var idcontrole = "";
var title = "";
var label = "";
var prefixo = "";
var sufixo = "";

CKEDITOR.plugins.add('simpleLink',
{
    init: function (editor) {
        editor.addCommand('simpleLinkDialog', new CKEDITOR.dialogCommand('simpleLinkDialog'));
        editor.ui.addButton('SimpleLink',
            {
                label: label,
                command: 'simpleLinkDialog',
                icon: this.path + 'images/icon.png'
            });
    }
});


//dialog para o plugin
CKEDITOR.dialog.add('simpleLinkDialog', function (editor) {
    return {
        title: title,
        minWidth: 400,
        minHeight: 200,
        contents:
		[
			{
			    id: 'general',
			    label: 'Settings',
			    elements: elements
			}
		],
        onOk: function () {
            dialog = this;

            for (var i = 0; i < elements.length; i++) {
                var valor = dialog.getValueOf('general', elements[i].id);
                editor.insertHtml(prefixo + valor + sufixo);
            }
        }
    };
});
