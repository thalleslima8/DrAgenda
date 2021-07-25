'use strict';

//kendo.culture("pt-BR");
Globalize.locale("pt-BR");

$.blockUI.defaults.message = '<img src="/img/loading.svg" />';
$.blockUI.defaults.baseZ = 100000;
$.blockUI.defaults.css.border = 'none';
$.blockUI.defaults.css.padding = '0';
$.blockUI.defaults.css.backgroundColor = 'transparent';

$.fn.select2.defaults.set("theme", "bootstrap4");
$.fn.select2.defaults.set("width", "100%");
$.fn.select2.defaults.set("dropdownAutoWidth ", "true");

app.config({

    /*
    |--------------------------------------------------------------------------
    | Autoload
    |--------------------------------------------------------------------------
    |
    | By default, the app will load all the required plugins from /assets/vendor/
    | directory. If you need to disable this functionality, simply change the
    | following variable to false. In that case, you need to take care of loading
    | the required CSS and JS files into your page.
    |
    */

    autoload: true,

    /*
    |--------------------------------------------------------------------------
    | Provide
    |--------------------------------------------------------------------------
    |
    | Specify an array of the name of vendors that should be load in all pages.
    | Visit following URL to see a list of available vendors.
    |
    | https://thetheme.io/theadmin/help/article-dependency-injection.html#provider-list
    |
    */

    provide: [],

    /*
    |--------------------------------------------------------------------------
    | Google API Key
    |--------------------------------------------------------------------------
    |
    | Here you may specify your Google API key if you need to use Google Maps
    | in your application
    |
    | Warning: You should replace the following value with your own Api Key.
    | Since this is our own API Key, we can't guarantee that this value always
    | works for you.
    |
    | https://developers.google.com/maps/documentation/javascript/get-api-key
    |
    */

    googleApiKey: 'AIzaSyBpDN7duVRavEIBq78czYoENktkNtyUbeg',

    /*
    |--------------------------------------------------------------------------
    | Google Analytics Tracking
    |--------------------------------------------------------------------------
    |
    | If you want to use Google Analytics, you can specify your Tracking ID in
    | this option. Your key would be a value like: UA-XXXXXXXX-Y
    |
    */

    googleAnalyticsId: 'UA-73325209-2',

    /*
    |--------------------------------------------------------------------------
    | Smooth Scroll
    |--------------------------------------------------------------------------
    |
    | By changing the value of this option to true, the browser's scrollbar
    | moves smoothly on scroll.
    |
    */

    smoothScroll: false,

    /*
    |--------------------------------------------------------------------------
    | Save States
    |--------------------------------------------------------------------------
    |
    | If you turn on this option, we save the state of your application to load
    | them on the next visit (e.g. make topbar fixed).
    |
    | Supported states: Topbar fix, Sidebar fold
    |
    */

    saveState: false,

    /*
    |--------------------------------------------------------------------------
    | Cache Bust String
    |--------------------------------------------------------------------------
    |
    | Adds a cache-busting string to the end of a script URL. We automatically
    | add a question mark (?) before the string. Possible values are: '1.2.3',
    | 'v1.2.3', or '123456789'
    |
    */

    cacheBust: '',



});

/*
|--------------------------------------------------------------------------
| Application Is Ready
|--------------------------------------------------------------------------
|
| When all the dependencies of the page are loaded and executed,
| the application automatically call this function. You can consider it as
| a replacer for jQuery ready function - "$( document ).ready()".
|
*/

app.ready(function () {

    $("form").submit(function (event) {
        $.blockUI();
        var isValid = $(this).valid();
        if (isValid) {
            return true;
        }
        else {
            event.preventDefault();
            $.unblockUI();
            return false;
        }
    });

    var phoneMask = function (phone, ev, el, op) {
        var masks = ['(00) 0000-00000', '(00) 0 0000-0000'];
        phone = phone.replace(/\D/g, '');
        var mask = (phone.length > 10) ? masks[1] : masks[0];
        el.mask(mask, op);
    };

    var phoneMaskOptions = {
        onKeyPress: function (phone, ev, el, op) {
            phoneMask(phone, ev, el, op);
        }
    };

    $('.phone-mask').each(function (i, obj) {
        phoneMask($(obj).val(), null, $(obj), phoneMaskOptions);
    });

    var cpfCnpjMask = function (cpfCnpj, ev, el, op) {
        var masks = ['000.000.000-000', '00.000.000/0000-00'];
        cpfCnpj = cpfCnpj.replace(/\D/g, '');
        var mask = (cpfCnpj.length > 11) ? masks[1] : masks[0];
        el.mask(mask, op);
    };

    var cpfCnpjMaskOptions = {
        onKeyPress: function (cpfCnpj, ev, el, op) {
            cpfCnpjMask(cpfCnpj, ev, el, op);
        }
    };

    $('.cpfCnpj-mask').each(function (i, obj) {
        cpfCnpjMask($(obj).val(), null, $(obj), cpfCnpjMaskOptions);
    });

    $('.cpf-mask').each(function (i, obj) {
        $(obj).mask("000.000.000-00");
    });

    $('.cnpj-mask').each(function (i, obj) {
        $(obj).mask("00.000.000/0000-00");
    });

    $('.only-numers').keypress(function (e) {
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $('.only-numers-x').keypress(function (e) {
        if (e.which !== 8 && e.which !== 0
            && e.which !== 88 && e.which !== 120
            && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $('.cep-mask').each(function (i, obj) {
        $(obj).mask("00000-000");
    });

    $('.money-mask').each(function (i, obj) {
        $(obj).mask("#.##0,00", { reverse: true });
    });

    $('.date-picker').kendoMaskedDatePicker();

    /*
    |--------------------------------------------------------------------------
    | Plugins
    |--------------------------------------------------------------------------
    |
    | Import initialization of plugins that used in your application
    |
    */

    //require('./plugins/something.js');

    /*
    |--------------------------------------------------------------------------
    | Paritials
    |--------------------------------------------------------------------------
    |
    | Import your main application code
    |
    */

    //require('./partials/something.js');

});
