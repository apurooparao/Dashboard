function pageLoad(sender, args) {
    if (args.get_isPartialLoad()) {
        jQuery(document).ready(function () {
            jQuery("#txtIntime").datepicker({ minDate: 0 });

            $('.timepicker_input').timepicker({
                showPeriod: true,
                showLeadingZero: true
            });
        });
    }
}
