# RallyCat
RallyCat Refactor!


How to Setup

0. Setup a new azure blob for kanban images
1. Modify RallyCat.WebApi/config.json.template, add your rally project settings (you may need to refer rally rest api help to get those infos), account info, slack channels, azure blob key/ref ...
2. Rename config.json.tempate -> config.json
3. Publish webapi website to to a public url, e.g. rallycat.xxxx.com
4. Add a new outbound webhook integration to your channels matching config.json, set Post Url {baseUrl}/api/Rally/Details (e.g. rallycat.xxxx.com/api/Rally/Details, set up keyword (e.g. rally:) and other settings
5. call rallycat in your channel:
    rally: DE1234
    rally: US100
    rally: kanban
