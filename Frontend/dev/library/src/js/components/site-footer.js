export class SiteFooter {

    static main() {

        $('.site-footer .copyright-year').text(new Date().getFullYear().toString());
    }
}