def sumofnumbers(n):
	if n == 0:
		return 0
	else:
		return sumofnumbers(n // 10) + n % 10
		
if __name__ == '__main__':
	n = input()
	print sumofnumbers(n)