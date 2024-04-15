export class DatePicker {
    
    constructor() {
        this.datepickers = $('.datepicker');
        if (this.datepickers.length) {
            this.init();
        }
    }

    init() {
        this.datepickers.each(function() {
            $(this).datepicker();            
        });
    }

}