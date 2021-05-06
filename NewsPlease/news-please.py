import json
import os
import sys
from newsplease import NewsPlease

name = 'LinkList.txt'
basepath = sys.argv[1]

download_dir = basepath + 'Data' + '/'
os.makedirs(download_dir)

articles = NewsPlease.from_file(basepath + name)

for url in articles:
    article = articles[url]
    print("Downloading " + article.url)
    with open(download_dir + article.filename + '.json', 'w') as outfile:
        json.dump(article.get_serializable_dict(), outfile, indent=4, sort_keys=True)