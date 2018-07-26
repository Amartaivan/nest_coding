if __name__ == "__main__":
    n = input()
    result = 1
    
    if n % 2 == 0:
        for i in range(2, n + 1, 2):
            result *= i
    else:
        for i in range(1, n + 1, 2):
            result *= i
    print result