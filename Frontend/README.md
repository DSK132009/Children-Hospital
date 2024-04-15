# Introduction 
This Frontend Boilerplate uses Grunt & Nunjucks & Express to spin up a local server with live reload.  

Last major update by:  DJ Hughes.  September 2019

# Getting Started
1.  Install Node.js
2.  Open powershell in projects Frontend folder.
3.  Run "npm install"
	This will add "node_modules" which has all the necessary modules for this project.
4.  Run "npm update" to update node modules
4.  Run "grunt" or optionally use a random port to have multiple grunts running.  ex:  "grunt --port=3025"

		This will run the project and compile all the SASS and JS, as well as copy the /library folder to both "/public" and "static_html".  This will also compile all nunjuck templates in dev/App/Templates to static html and place them in "static_html".

		static_html will be recompiled any time a SASS or JS file is modified.  It will not compile currently if a template file is modified, so you'll have to re-run "grunt" or save a sass/js file.
5.  You can also run "grunt html" if you want to just recompile the "static_html" folder 

# Repo
https://silvertech.visualstudio.com/Front-End%20Starter%20Project/_git/Front-End%20Starter%20Project%202019

# Instructions

	You should use .html files as templates/modules even though we are using nunjucks.  Any links just include filename.html, so when it compiles to static html the links will be wired up.

# HTML Folder Structure

	All .html files reside in dev/App folder

MasterPages - This should be the core start of every html files that gets extended from each /Templates file.  You can use as many master files as you want with this grunt process.  Simply extend the desired master for each template.

Templates - Grunt pulls any files from this folder and creates "pages" to use with localhost url.  All "design templates" should go here and import either a layout or a partial.  Each template should have a comp thumbnail linked to it from the index.html so we can visually see any templates created.

views - This is where all module/components/layouts should go.  You'll spend most of your time in here developing modules to import from your /Templates files.

  views/layouts - This should be used for any module layouts that import other partial files.  A good example would be if you have a Blog Listing page.  You would create the layout here and then import each individual partial blog post.  Use folders to stay organized

  views/partials - This is where all modules/components go that will either be imported from your Templates file or from a layout.  Use folders to stay organized

  views/shared - This should contain any modules/layouts/components that are typically shared throughout the site, such as your header/footer/menu


# SASS Folder Structure
	
	All sass files reside in /library/src/scss

Base: holds some global boilerplate code for the project. Including standard styles such as buttons & typographic rules, which are commonly used throughout your project.

Components (or modules): holds all of your styles for sliders, ctas, cards and similar page components (think widgets). Your project will typically contain a lot of component files — as the whole site/app should be mostly composed of small modules.  Use folders to stay organized.

Layout: contains all styles involved with the layout of your project. Such as styles for your header, footer, navigation.

Pages: any styles specific to individual pages will sit here. For example it’s not uncommon for the home page of your site to require page specific styles that no other page receives.

Vendors: contains all third party code from external libraries and frameworks — such as Bootstrap, Slider plugins, jQueryUI, etc. However, there is often a need to override vendor code. If this is required, its good practice to use folder vendor-overrides and then name any files after the vendors they overwrite. 