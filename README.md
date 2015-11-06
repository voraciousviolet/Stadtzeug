# Stadtzeug

**Stadtzeug** (English: _City Stuff_) is the city simulation engine used in Sanity Plea, an upcoming game about city life in a gloriously corrupted reality.

The engine is intended to perform the following tasks:
* Preliminary metadata generation (city name, starting time, etc.)
* Company and product line generation
* Adaptive media outlets (newspaper, radio, police)
* Lazy, persistent citizen generation and population state management
  * Name, birth date, sex, etc. per person
  * Criminal records
  * Personality traits
  * In-memory caching of active citizens
* Store inventories
* Criminal activity tracking
* Quest generation and progression

## Compiling

Compile the Rant package using `rave pack` in the `/rant/` directory.
Then build the `Stadtzeug` project with msbuild or similar and you're good to go.

**Note:** The Stadtzeug package requires Rantionary 2.0.0 or later.
