import random
import sys

#f(x) = ax + b
def f(a, x, b):
	return a * x + b

if __name__ == "__main__":
	if "--nprint" in sys.argv or "-n" in sys.argv:
		n = input()
		a = input()
		b = input()
	else:
		print """This program generates n random numbers x, y
	y = a * x + b"""

		n = input("How much numbers do you want: ")
		a = input("Enter a: ")
		b = input("Enter b: ")
	
	output_x = ""
	output_y = ""
	for i in range(n):
		x = random.randint(10, 10000)
		output_x += str(x) + ' '
		output_y += str(f(a, x, b)) + ' '
	print output_x
	print output_y
	