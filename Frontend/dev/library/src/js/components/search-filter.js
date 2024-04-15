export class SearchFilter {

    static main() {
        
        function openDropdown(element) {

            const dropdown = $(element).closest('.dropdown').find('.dropdown-menu');
            $(element).closest('.dropdown').addClass('show');
            $(element).addClass('show');
            $(dropdown).addClass('show');
            
            $(element).text('CLOSE FILTER');
        }

        function closeDropdown(element) {

            const dropdown = $(element).closest('.dropdown').find('.dropdown-menu');
            $(element).closest('.dropdown').removeClass('show');
            $(element).removeClass('show');
            $(dropdown).removeClass('show');

            $(element).text('SEARCH FILTER');
        }

        function clearFilters(event, element) {
            event.preventDefault();

            $('#keywordsInput').val('');
            $('#categoryInput').val('0');

            //$(element).closest('.search-filter-form').trigger('reset'); 
        }

        $('.search-filter').each(function(index, element) {

            // const clearFiltersBtn = $(element).find('.clear-filters');
            // $(clearFiltersBtn).on('click', function(event) {
                // clearFilters(event, clearFiltersBtn);
            // });

            const dropdownToggle = $(element).find('.dropdown-toggle');
            $(dropdownToggle).on('click', function(event) {
                
                if (window.innerWidth > 1200)
                    return;

                const linkItem = event.currentTarget;

                if ($(linkItem).closest('.dropdown').hasClass('show')) { 
                    closeDropdown(linkItem);
                } else {
                    openDropdown(linkItem);
                }
            });

            $(dropdownToggle).on('keyup', function(event) {
                const linkItem = event.currentTarget;

                if (event.which === _KEYBOARD_.enter) {
                    openDropdown(linkItem);   
                } else if (event.which === _KEYBOARD_['arrow-u']) {
                    closeDropdown(linkItem);
                } else if (event.which === _KEYBOARD_['arrow-d']) {
                    openDropdown(linkItem);
                } else {
                    return false;
                }
            });
        });
    }

}