//source: http://greenicicleblog.com/2011/02/28/fixing-non-localizable-validation-messages-with-javascript/

(function ($) {
    
    // Walk through the adapters that connect unobstrusive validation to jQuery.validate
    // Look for all adapters that perform number validation
    
    $.each($.validator.unobtrusive.adapters, function () 

        if (this.name === "number") {
            
            // Get the method called by the adapter, and replace it with 
            // that changes the message to the jQuery.validate default 
            // that can be globalized. If that string contains a {0} placeholder,
            // it is replaced by the field name.
            
            var baseAdapt = this.adapt
            this.adapt = function (options) {
                
                var fieldName = new RegExp("Must be a number").exec(options.message);
                options.message = $.validator.format($.validator.messages.number, fieldName);
                baseAdapt(options);
                
            };

        }
        
    });
    
} (jQuery));
