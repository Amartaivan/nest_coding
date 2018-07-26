if __name__ == '__main__':
	for a in range(1, 10):
		for b in range(10):
			for c in range(10):
				if a != b and a != c and b != c:
					print a * 100 + b * 10 + c