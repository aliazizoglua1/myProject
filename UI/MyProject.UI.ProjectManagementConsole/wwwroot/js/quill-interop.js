// Custom Quill Editor JavaScript for Blazor
window.initQuillEditor = function (element, options) {
    try {
        if (typeof Quill === 'undefined') {
            console.error('Quill is not loaded');
            return null;
        }

        const config = {
            theme: options.theme || 'snow',
            placeholder: options.placeholder || 'Enter text here...',
            readOnly: options.readOnly || false,
            modules: {
                toolbar: [
                    [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
                    ['bold', 'italic', 'underline', 'strike'],
                    [{ 'color': [] }, { 'background': [] }],
                    ['blockquote', 'code-block'],
                    [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                    [{ 'indent': '-1'}, { 'indent': '+1' }],
                    [{ 'align': [] }],
                    ['link', 'image'],
                    ['clean']
                ]
            }
        };

        const quill = new Quill(element, config);
        
        console.log('Quill editor initialized successfully');

        return {
            getHTML: function() {
                return quill.root.innerHTML;
            },
            setHTML: function(html) {
                quill.root.innerHTML = html || '';
            },
            getText: function() {
                return quill.getText();
            },
            clear: function() {
                quill.setText('');
            },
            focus: function() {
                quill.focus();
            },
            enable: function(enabled) {
                quill.enable(enabled !== false);
            }
        };
    } catch (error) {
        console.error('Error initializing Quill editor:', error);
        return null;
    }
}; 