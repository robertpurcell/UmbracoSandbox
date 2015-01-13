# Zone Boilerplate #

## Folder Structure

    |--boilerplate
        |-- node_modules (not in repo)
        |-- src
        |-- dist (created when grunt prod is run)
        |-- favicon.ico
        |-- humans.txt
        |-- index.html
        |-- package.json
        |-- references.md


`|-- node_modules` all modules required for Grunt (this is added when setting up)

`|-- src` contains our javascript and scss for this project.

`|-- dist` contains all compiled JavaScript and css

`|-- favicon.ico` is the basic favicon for the site - **don't forget it**.

`|-- humans.txt` tells us who has worked on the project, who helped, and what technologies were used.

`|-- index.html` is the basic page markup (distilled from the HTML5 Boilerplate).

`|-- references.md` is a list of links researched to inform this template


Note: Please view README.md files within each folder for more info


## Setting up the project

|-- package.json is set to install the latest module versions compatible with grunt~0.4.1.

*Run (in this directory):*

    npm install

This will install all the node modules you require.


*To begin working on the project run:*

    grunt

## Modernizr

*If needs be this task can be run using:*

    grunt modernizr

This will build your custom modernizr file based on features you have used in your css and js

## Installing new packages

**For new Grunt modules run:**

	npm install <package name> --save-dev

The flag `--save-dev` will save the version number to package.json so that everyone has the same versions.

**For new Compass plugins (or Ruby Gems) run:**

    gem install <plugin name>

Then inside the grunt file under compass, options add

    require '<plugin name>'

and then import it into the Sass file

    @import "<plugin name>"
