with open(input("enter the file path: "), "w+") as myFile:
                fileToWrite = '{"items":['
                while input("type y to enter another line") == "y":
                    fileToWrite += "{"
                    name = input("Name: ")
                    fileToWrite += f'"name":"{name}",'
                    dialogue = input("Dialogue: ")
                    fileToWrite += f'"dialogue":"{dialogue}",'
                    portrait = input("Portrait: ")
                    fileToWrite += f'"portrait":"{portrait}"'
                    fileToWrite += "},"
                    
                fileToWrite += "]}"
                myFile.write(fileToWrite)
