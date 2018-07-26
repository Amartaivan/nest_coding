def print_num(n, counter):
	if counter == n:
		print n
		return
	print counter
	print_num(n, counter + 1)
	
if __name__ == '__main__':
	n = input()
	print_num(n, 0)