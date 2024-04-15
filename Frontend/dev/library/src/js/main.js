/* Site Specific Javascript Component Imports */
import { ImageLoader } from './utils/ImageLoader';
import { SiteHeader } from './components/site-header';
import { SiteFooter } from './components/site-footer';
import { StatCards } from './components/stat-cards';
import { LatestNewsArticles } from './components/latest-news-articles';
import { QuickCareLocator } from './components/quick-care-locator';
import { QuickLinksDropdowns } from './components/quick-link-dropdown';
import { VideoEmbedController } from './components/video-embed';
import { VideoSection } from './components/video-section';
import { SearchFilter } from './components/search-filter';
import { UmcCareFilter } from './components/umc-care-filter';
import { DatePicker } from './components/datepicker';
import { GenericHero } from './components/generic-hero';
import { AllArticles } from './components/all-articles';

const MainScripts = (() => {

	// Shared
	if (typeof svg4everybody == 'function') { svg4everybody(); }

	const imageLoader = new ImageLoader();
	imageLoader.initialize();

	SiteHeader.main();
	SiteFooter.main();
	StatCards.main();
	LatestNewsArticles.main();
	QuickCareLocator.main();
	QuickLinksDropdowns.main();
	VideoEmbedController.main();
	VideoSection.main();
	SearchFilter.main();
	GenericHero.main();
	new DatePicker();
	new AllArticles();
	
	window.locationsReady = function() {
		UmcCareFilter.main();
	};
	
})();
