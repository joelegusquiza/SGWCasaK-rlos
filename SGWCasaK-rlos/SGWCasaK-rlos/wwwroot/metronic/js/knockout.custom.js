ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {}, $el = $(element);

        options.language = "es";

        $el.datepicker(options);

        //handle the field changing by registering datepicker's changeDate event
        ko.utils.registerEventHandler(element, "changeDate", function () {
            var observable = valueAccessor();
            observable($el.datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $el.datepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            $el = $(element);

        //handle date data coming via json from Microsoft
        if (String(value).indexOf('/Date(') == 0) {
            value = new Date(parseInt(value.replace(/\/Date\((.*?)\)\//gi, "$1")));
        }

        var current = $el.datepicker("getDate");

        if (value - current !== 0) {
            $el.datepicker("setDate", new Date(value));
        }
    }

  
};
 
ko.bindingHandlers.timepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize timepicker with some optional options
        var options = allBindingsAccessor().timepickerOptions || {};
        $(element).timepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "changeTime.timepicker", function (e) {
            var observable = valueAccessor();
            var current = ko.utils.unwrapObservable(observable);

            observable(e.time.value);
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).timepicker("destroy");
        });
    },

    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        // calling timepicker() on an element already initialized will
        // return a TimePicker object
        var el = $(element);
        var date = new Date(value);
        if (date == "Invalid Date")
            el.timepicker("setTime", value);
        else
            el.timepicker("setTime", date.getHours() + ":" + date.getMinutes());
    }
};

ko.bindingHandlers.editableText = {
    init: function (element, valueAccessor) {
        $(element).on("blur", function () {
            var observable = valueAccessor();
            if (observable != null) {
                observable($(this).text());
            }
           
        });
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).text(value);
    }
};

ko.bindingHandlers.footable = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        $(element).closest("table").footable();
    },
    update: function (element, valueAccessor) {
        //this is called when the observableArray changes
        //and after the foreach has rendered the contents       
        ko.unwrap(valueAccessor()); //needed so that update is called
        $(element).closest("table").trigger('footable_redraw');
    }
}

ko.bindingHandlers.footable.preprocess = function (value, name, addBindingCallback) {
    //add foreach binding
    addBindingCallback('foreach', '{ data: ' + value + '}');
    return value;
}