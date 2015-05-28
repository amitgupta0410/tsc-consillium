$(function () {
   $('.daterange').each(function (i, el) {
        $(el).daterangepicker({
            timePicker: false,
            format: 'DD MMM YYYY'
        });
    });

    $('.date-max').each(function (i, el) {
        var maxDate = $(el).data('maxDate');
        $(el).datetimepicker({
            timepicker: false,
            format: 'd M Y',
            maxDate: maxDate

        });
    });

    $('.date-min').each(function (i, el) {
        var minDate = $(el).data('minDate');
        $(el).daterangepicker({
            timePicker: false,
            singleDatePicker: true,
            format: 'DD MMM YYYY',
            minDate: minDate
        });
    });

    $('.date').daterangepicker({
        timePicker: false,
        singleDatePicker: true,
        timePickerIncrement: 15,
        format: 'DD MMM YYYY'
    });

    $('.datetime').daterangepicker({
        timePicker: true,
        singleDatePicker: true,
        timePickerIncrement: 15,
        format: 'DD MMM YYYY h:mm A'
    });

//
//    $(function () {
//        $('.daterange').daterangepicker({
//            timePicker: false,
//            format: 'DD/MM/YYYY'
//        },
//            function (start, end, label) {
//                var startDate = start.format('YYYY-MM-DD');
//                var endDate = end.format('YYYY-MM-DD');
//                $('#startAt').val(startDate);
//                $('#endAt').val(endDate);
//            }
//        ).on('apply.daterange', function (ev, picker) {
//            var startDate = picker.startDate.format('YYYY-MM-DD');
//            var endDate = picker.endDate.format('YYYY-MM-DD');
//            $('#startAt').val(startDate);
//            $('#endAt').val(endDate);
//        });
//    });
});

//$(function changeTimeToDate() {
//    console.log("hasdkjhkjashdkjasljdkl");
//    var combineDate = $('#combine-date').val();
//    var fromHours = $('#from-hours').val();
//    var fromminutes = $('#from-minutes').val();
//    var fromDate = combineDate + " " + fromHours + ":" + fromminutes;
//    var toHours = $('#to-hours').val();
//    var tominutes = $('#to-minutes').val();
//    var toDate = combineDate + " " + toHours + ":" + tominutes;
//    $('#fromDate').val(fromDate);
//    $('#toDate').val(toDate);
//});
