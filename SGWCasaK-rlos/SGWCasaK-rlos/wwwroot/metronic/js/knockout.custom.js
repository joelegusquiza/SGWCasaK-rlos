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

ko.bindingHandlers.datetimepicker = {
	init: function (element, valueAccessor, allBindings) {
		//initialize datepicker with some optional options
		var format;
		var defaultFormat = 'yyyy-mm-ddThh:ii:ss a';
		if (typeof allBindings == 'function') {
			format = allBindings().format || defaultFormat;
		}
		else {
			format = allBindings.get('format') || defaultFormat;
		}
		$(element).datetimepicker({
			autoclose: true,
			todayBtn: true,
			'format': ' dd/mm/yyyy HH:ii ' 
		})

		//when a user changes the date, update the view model
		ko.utils.registerEventHandler(element, "changeDate", function (event) {
			var value = valueAccessor();
			if (ko.isObservable(value)) {
				var dateObject = new Date(event.date);
				console.log((new Date(dateObject.getTime() - new Date().getTimezoneOffset() * 60000)).toISOString().split('Z')[0] + '-0' + new Date().getTimezoneOffset() / 60+":00");
				value((new Date(dateObject.getTime() - new Date().getTimezoneOffset() * 60000)).toISOString().split('Z')[0] + '-0' + new Date().getTimezoneOffset() / 60 + ":00");
			}
		});
	},
	update: function (element, valueAccessor) {
		var date = ko.unwrap(valueAccessor());
		if (date) {
			$(element).datetimepicker('setValue', date);
		}
	}
};