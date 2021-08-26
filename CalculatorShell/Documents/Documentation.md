# Supported number formats

* Binary - `0b1010`
* Octal - `0o777`
* Hexa - `0hff`
* Locical - `true` or `false`
* Date - `year/month/day hour:minute:second`
* Floating point or integer values in base ten: `1000`, `1001.1`
* Floating point values in Scientific notation: `10E3`
* Floating point values with SI prefixes.

## Grouping

Number digits can be grouped via the `_` char. E.g: `1_000` and `10_00` with both parse to 1000. The `_` char can used regardless of the system. E.g: `0b_10_00`, `0hff_ad`.

## Prefixes
Numbers can be given with prefixes: `1000_p` will parse to 1000 pico which will be 1000E-12. Note: the SI prefix and the number must be spepperated by the `_` char.

Currently recognized SI prefixes:

| prefix | Value |
| :----- | :---- |
| _p     | 1E-12 |
| _n     | 1E-9  |
| _micro | 1E-6  |
| _m     | 1E-3  |
| _d     | 1E1   |
| _h     | 1E2   |
| _k     | 1E3   |
| _M     | 1E6   |
| _G     | 1E9   |
| _T     | 1E12  |

# Variables

Variable names can contain the letters of the english alphabet (a-z), numbers (0-9) and the following symbols:
`_` `$` `-` `*`

A variable name can't start with numbers.

If a variable name starts with the `$` char, then it's treated as an expression.
