# UnityViewJson

A Unity package to view Json data using the builtin UI system

[Changelog](./CHANGELOG.md) - [License](./LICENSE.md) - [Repo](https://github.com/fhstp/UnityViewJson)

---

## Usage

### Data format

First create a json document describing the shape of the data you want to 
display along with formatting information. The schema for this document can
be found [here](./format.schema.json).

You can parse your format document using the `DataFormat.TryParse` function.

### Display style

You also need to choose some styling options for your data. These are configured
using an instance of the `DataStyle` class and allow you to change things like
text color.

### View Json

First make sure you have prepared the [format](#data-format) and 
[style](#display-style) for the data. Next prepare the `RectTransform` you want
to display the data in. ViewJson will attempt to display the data by fitting into
this rect, so make sure it has sufficient size.

Finally to view your data, call `ViewJson.TryViewJsonIn`. You can monitor the
returned code for any errors.