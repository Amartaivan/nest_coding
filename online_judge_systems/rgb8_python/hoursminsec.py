if __name__ == '__main__':
    s = int(raw_input())
    output = ""

    output += str(s / 3600) + ' ' + str(s / 60 % 60) + ' ' + str(s % 60)
    print output