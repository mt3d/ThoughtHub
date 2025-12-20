# A medium.com clone, built using C# and ASP.NET Core.

**ThoughtHub** is a demo project inspired by Medium.com.

**Disclaimer:** This project is created **for educational and learning purposes only**.

## Organization
- ThoughtHub.Api: The main backend API built with ASP.NET Core API
- ThoughtHub.Ui.BlazorWasm: A frontend built as a Standalone Blazor WebAssembly app

## Building & Running

1. Clone this repository or download a ZIP archive of it.

2. The default URLs for the frontend and the backend are:

    * `ThoughtHub.Api` app (`PlatformUrls:BackendUrl`): `http://localhost:5120`
    * `ThoughtHub.Ui.BlazorWasm` app (`PlatformUrls:BlazorWasmFrontendUrl`): `http://localhost:5220`

3. You can use the existing URLs or update them in the appsettings.json file of each project with new `BackendUrl` and `BlazorWasmFrontendUrl` endpoints:

    * `ThoughtHub.Api` file in the root of the `backend` app.
    * `wwwroot/appsettings.json` file in the `ThoughtHub.Ui.BlazorWasm` app.

4. Run the `ThoughtHub.Api` and `ThoughtHub.Ui.BlazorWasm` apps.
    - Optionally,

5. Navigate to the `ThoughtHub.Ui.BlazorWasm` app at the `BlazorWasmFrontendUrl`.

6. Register a new user or use one of the preregistered test users if the seed option was used.


## Roadmap
Medium.com is a huge website, and trying to fully recreate it would takes hundreds of hours. Below is a list of some of the features available in Medium.com, separated into small steps that could be built indiviually.

### Content Model

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
- The system should avoids the growth of duplicate or misspelled tags
