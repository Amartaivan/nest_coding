def print_num(a, b):
	if a < b:
		print a
		print_num(a + 1, b)
	elif a > b:
		print a
		print_num(a - 1, b)
	else:
		print b
		
if __name__ == '__main__':
	a = input()
	b = input()
	print_num(a, b)