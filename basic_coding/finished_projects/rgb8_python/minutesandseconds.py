if __name__ == '__main__':
    x = int(raw_input())
    output = ""

    output += str(x / 60) + ' ' + str(x % 60)
    print output