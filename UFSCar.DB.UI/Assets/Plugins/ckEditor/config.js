/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.extraPlugins = 'simpleLink,simpleuploads';
    config.filebrowserUploadUrl = '/UploadCkEditor.ashx?Type=File';
    config.filebrowserImageUploadUrl = '/UploadCkEditor.ashx?Type=Image';
    config.filebrowserFlashUploadUrl = '/UploadCkEditor.ashx?Type=Flash';
};