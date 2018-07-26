def ispowerof2(n):
	if n == 2:
		return True
	elif n % 2 != 0:
		return False
	return ispowerof2(n / 2)
	
if __name__ == '__main__':
	n = input()
	print ispowerof2(n)