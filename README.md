# The Reactive Machine website and documentation

This repo houses the assets used to build the website at https://reactive-machine.org, including the project's documentation.

## Running the site locally

In order to run the site locally, you need to have [Yarn](https://yarnpkg.com) and [Hugo](https://gohugo.io) installed (in particular the Hugo version listed in [`netlify.toml`](netlify.toml)). With those tools installed:

```shell
make serve
```

## Working on the documentation

All documentation for the site is in the [`content/docs`](content/docs) folder. Each document has three attributes:

* A `title`
* A `description` (supports Markdown)
* An optional `weight` field used to determine the order in which the docs are listed in the nav

The list of features listed on the home page is in [`data/features.yaml`](data/features.yaml). The `description` field supports Markdown.

The URL for the image on the main page is set using the `params.imgUrl` parameter in [`config.toml`](config.toml).

## Publishing the site

The site is published automatically by [Netlify](https://netlify.com). Whenever you push changes to `master`, the site will be rebuilt and redeployed. The site *cannot* be published manually.
