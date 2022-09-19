import bpy
import sys
import math
import os
from mathutils import Euler

axis = sys.argv[sys.argv.index('--') + 1]

rad000 = 0
rad180 = math.radians(180)
radP90 = math.radians(+90)
radN90 = math.radians(-90)

rotations = {
    "Z+": Euler([radP90, rad000, rad000], "XZY"),
    "Z-": Euler([radP90, rad000, rad180], "XZY"),
    "X+": Euler([radP90, rad000, radN90], "XZY"),
    "X-": Euler([radP90, rad000, radP90], "XZY"),
    "Y+": Euler([rad180, rad000, rad000], "XZY"),
    "Y-": Euler([rad000, rad000, rad000], "XZY"),
}

camera = bpy.data.objects["Camera"]
    
camera.rotation_mode = 'QUATERNION'
camera.rotation_quaternion = rotations[axis].to_quaternion()

bpy.context.scene.render.image_settings.file_format = 'PNG'
bpy.ops.render.render()

filepath = os.path.abspath("face" + axis + ".png")

bpy.data.images['Render Result'].save_render(filepath=filepath)

