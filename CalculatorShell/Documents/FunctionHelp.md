# abs

Returns the absolute value of the input parameter. Works with regular and 
fraction numbers.

In mathematics, the absolute value or modulus of a real number x, denoted
|x|, is the non-negative value of x without regard to its sign. For example,
the absolute value of 3 is 3, and the absolute value of −3 is also 3. 
The absolute value of a number may be thought of as its distance from zero.

## usage examples

```
abs(-2)
abs(5)
```

# and

Perform bitwise AND on two numbers. Works with regular and 
fraction numbers.

A bitwise AND is a binary operation that takes two equal-length binary
representations and performs the logical AND operation on each pair of the 
corresponding bits, which is equivalent to multiplying them. Thus, if both bits
in the compared position are 1, the bit in the resulting binary representation 
is 1 (1 × 1 = 1); otherwise, the result is 0 (1 × 0 = 0 and 0 × 0 = 0).

## usage example

```
and(5;2)
```

# arccos

Perfrom inverse cosine on the input number. Works with regular and 
fraction numbers.

The value will be retuned in the current angle system which can be
changed with the mode command.

## usage example

```
arccos(1)
```

# arcctg

Perfrom inverse cotangent on the input number. Works with regular and 
fraction numbers.

The value will be retuned in the current angle system which can be
changed with the mode command.

## usage example

```
arcctg(1)
```

# arctan

Perfrom inverse tangent on the input number. Works with regular and 
fraction numbers.

The value will be retuned in the current angle system which can be
changed with the mode command.

## usage example

```
arctan(1)
```

# arcsin

Perfrom inverse sine on the input number. Works with regular and 
fraction numbers.

The value will be retuned in the current angle system which can be
changed with the mode command.

## usage example

```
arcsin(1)
```

# ceil

Returns the smallest integral value greater than or equal to the specified
number.

## usage example

```
ceil(4.2)
```

# cos

Calculate the cosine of an angle.

The value will be retuned in the current angle system which can be
changed with the mode command.

## usage example

```
cos(90)
```

# cplx

Create a complex number. The first argument is the real value and
the second number is the imaginary value.

## usage example

```
cplx(2;2)
```

# cplxp

Create a complex number given in polar coordinates. 
The first argument is the magnitude and the second number is the angle.

Angle is interpreted in the current angle system. which can be
changed with the mode command.

## usage example

```
cplxp(2;2)
```

# ctg

Calculate the cotangent of an angle.

The value will be retuned in the current angle system which can be
changed with the mode command.

## usage example

```
ctg(90)
```

# deg2grad

Converts angle in degrees to angle in gradians

## usage example

```
deg2grad(90)
```

# deg2rad

Converts angle in degrees to angle in radians

## usage example

```
deg2rad(90)
```

# floor

Returns the largest integral value less than or equal to the specified number.

## usage example

```
floor(4.2)
```

# grad2deg

Converts angle in gradians to angle in degrees

## usage example

```
grad2deg(100)
```

# grad2rad

Converts angle in gradians to angle in radians

## usage example

```
grad2rad(100)
```

# log

Return the logarithm of a number in the specified base.

The logarithm is the inverse function to exponentiation. That means the logarithm
of a given number x is the exponent to which another fixed number, the base b,
must be raised, to produce that number x. In the simplest case, the logarithm
counts the number of occurrences of the same factor in repeated multiplication.

## usage example

```
log(10;2)
```

# ln

Return the natural logarithm of a number.

The natural logarithm of a number is its logarithm to the base of the mathematical
constant e, which is an irrational and transcendental number approximately equal 
to 2.718281828459.

## usage example

```
ln(10)
```

# not

Perform bitwise NOT on a number. Works with regular and 
fraction numbers.

The bitwise NOT, or complement, is a unary operation that performs logical 
negation on each bit, forming the ones' complement of the given binary value.
Bits that are 0 become 1, and those that are 1 become 0.

## usage example

```
not(5)
```

# or

Perform bitwise OR on two numbers. Works with regular and 
fraction numbers.

A bitwise OR is a binary operation that takes two bit patterns of equal length
and performs the logical inclusive OR operation on each pair of corresponding
bits. The result in each position is 0 if both bits are 0, while otherwise the 
result is 1.

## usage example

```
or(5;2)
```

# rad2deg

Converts angle in radians to angle in degrees

## usage example

```
rad2deg(1)
```

# rad2grad

Converts angle in radians to angle in gradians

## usage example

```
rad2gad(1)
```

# root

Return the Nth root of a number. An nth root of a number x is a number 
r which, when raised to the power n, yields x.

## usage example

```
root(5;2)
```

# shl

Perform bitwise left shift on two numbers. Works with regular and 
fraction numbers.

A bitwise left shift is a binary operation that shifts the first arguments bits by
the second argument.

This operation discards the high-order bits that are outside the range of the result
and sets the low-order empty bit positions to zero.

## usage example

```
shl(1;2)
```

# shr

Perform bitwise right shift on two numbers. Works with regular and 
fraction numbers.

A bitwise right shift is a binary operation that shifts the first arguments bits by
the second argument.

This operation discards the low-order bits.

## usage example

```
shr(4;1)
```

# sign

Returns an integer that indicates the sign of a number.

Possible ouptuts:

-1 : value is less than zero. 
0  : value is equal to zero. 
1  : value is greater than zero.

## usage example

```
sign(-4)
```

# sin

Calculate the sine of an angle.

The value will be retuned in the current angle system which can be
changed with the mode command.

## usage example

```
sin(90)
```

# vect2

Create a 2D vector. The 2D vector supports the following operations:

* Addition between 2D vectors and regular numbers
* Subtraction between 2D vectors and regular numbers
* Multiplication between 2D vectors and regular numbers
* Division with a regular number

## usage example

```
vect2(5;2)
```

# vect3

Create a 3D vector. The 3D vector supports the following operations:

* Automatic conversion of a 3D vector to 3D vector with Z = 0,
* Addition between 3D vectors and regular numbers
* Subtraction between 3D vectors and regular numbers
* Multiplication between 3D vectors and regular numbers
* Division with a regular number

## usage example

```
vect3(5;2;4)
```

# xor

Perform bitwise XOR on two numbers. Works with regular and 
fraction numbers.

A bitwise XOR is a binary operation that takes two bit patterns of equal length
and performs the logical inclusive XOR operation on each pair of corresponding
bits. The result in each position is 0 if both bits are equal, while otherwise the 
result is 1.

## usage example

```
xor(5;2)
```