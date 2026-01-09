# A medium.com clone, built using C# and ASP.NET Core.

**ThoughtHub** is a project inspired by Medium.com. It all started out as a way to further deepen my understanding of .NET and ASP.NET Core, but then it became an attempt to recreate the full Medium website.

**Disclaimer:** This project is created **for educational and learning purposes only**.

## Organization
- ThoughtHub.Api: The main backend API built with ASP.NET Core API
- ThoughtHub.Ui.BlazorWasm: A frontend built as a Standalone Blazor WebAssembly app
- ThoughtHub.Api.LocalStorage: Module for saving uploaded images locally on the server.
- ThoughtHub.Api.AzureBlobStorage: Module for saving uplodaded images remotely on Azure Blob Storage
- ThoughtHub.Api.Core: The main entity classes.
- ThoughtHub.Api.Models: The models returned by the API. Used both on the backend and the frontend.

## Building & Running

1. Clone this repository or download a ZIP archive of it.

2. Run using `aspire run`

3. The main API project supports creating random seed content for testing purposes.

## Roadmap
Medium.com is a huge website, and trying to fully recreate it would takes hundreds of hours. Below is a list of some of the features I'm most interested in for now.

### Content Model
- Add support for comments
- Add support for reading lists
- Add support for topics/tags

### Text Editor
- A full text editor with structured blocks
	- Paragraphs
	- Headings
	- Images
	- Embeds
	- Quotes
	- Code blocks
- Deterministic serialization into HTML or Markdown
- Preservation of formatting consistency across browsers and devices

### Real-time autosave engine
- An engine that:
	- writes draft deltas at controlled intervals
	- merges competing changes
	- guards against race conditions when users edit the same draft on multiple devices

### Recommendation Engine
- A recommendation engine that computes related articles using multiple signals:
	- Topic similarity
	- Reading history
	- Collaborative filtering
	- Reading time estimation
	- Recency scoring

### Tagging System
- A tagging system with:
	- Hierarchical relationships
	- Tag synonyms
	- Moderation rules
	- Deterministic alogrithm for topic ranking
- The system should avoids duplicate or misspelled tags
