if __name__ == '__main__':
    n, t = map(int, raw_input().split())
    t %= n
    t = n - t

    output = ""
    for i in range(n):
        t += 1
        if t % n == 0:
            output += str(n) + ' '
        else:
            output += str(t % n) + ' '
    print output