BLENDER?=blender
CONVERT?=convert

stars.blend: base.blend stars.py bsc5.txt
	$(BLENDER) base.blend --background --python stars.py

faceX+.png: stars.blend takePicture.py
	$(BLENDER) stars.blend --background --python takePicture.py -- X+

faceX-.png: stars.blend takePicture.py
	$(BLENDER) stars.blend --background --python takePicture.py -- X-

faceY+.png: stars.blend takePicture.py
	$(BLENDER) stars.blend --background --python takePicture.py -- Y+

faceY-.png: stars.blend takePicture.py
	$(BLENDER) stars.blend --background --python takePicture.py -- Y-

faceZ+.png: stars.blend takePicture.py
	$(BLENDER) stars.blend --background --python takePicture.py -- Z+

faceZ-.png: stars.blend takePicture.py
	$(BLENDER) stars.blend --background --python takePicture.py -- Z-

cubemap.png: $(echo face{X,Y,Z}{-,+}.png)
	$(CONVERT) -append face{X,Y,Z}{+,-}.png cubemap.png
