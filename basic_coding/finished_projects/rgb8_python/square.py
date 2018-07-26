if __name__ == '__main__':
    a, b = map(int, raw_input().split())
    output = ""

    output += str(a * b) + ' ' + str((a + b) * 2)
    print output