def calculate(i, j):
    if i == 1 or j == 1:
        return 1
    else:
        return calculate(i - 1, j) + calculate(i, j - 1)

if __name__ == '__main__':
    n, m = map(int, raw_input().split())
    print calculate(n, m)