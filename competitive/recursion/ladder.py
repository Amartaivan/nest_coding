def calculate(n):
    if n < 3:
        return 1
    if n == 3:
        return 2

    if n - 3 >= 0:
        return calculate(n - 1) + calculate(n - 3)
    else:
        return calculate(n - 1)

if __name__ == "__main__":
    n = input()
    print calculate(n)