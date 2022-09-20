import bpy
import os
import math
from mathutils import Euler

starBase = bpy.data.objects["star.base"]

def make_star(line):
    if len(line) < 107:
        return

    strRAh = line[75:77].strip()
    strRAm = line[77:79].strip()
    strRAs = line[79:83].strip()
    strDEsign = line[83:84].strip()
    strDEd = line[84:86].strip()
    strDEm = line[86:88].strip()
    strDEs = line[88:90].strip()
    strVmag = line[102:107].strip()
    
    # if any of those value is empty, abort
    for value in [
            strRAh, strRAm, strRAs,
            strDEsign, strDEd, strDEm, strDEs,
            strVmag,
    ]:
        if len(value) == 0:
            return

    RAh = int(strRAh)
    RAm = int(strRAm)
    RAs = float(strRAs)
    DEsign = int(strDEsign + "1")
    DEd = int(strDEd)
    DEm = int(strDEm)
    DEs = int(strDEs)
    Vmag = float(strVmag)

    RAdeg = 15 * (RAh + RAm / 60 + RAs / 3600)
    DEdeg = DEsign * (DEd + DEm / 60 + DEs / 60)

    # create new star
    newStar = starBase.copy()
    newStar.animation_data_clear()
    bpy.context.collection.objects.link(newStar)
    newStar.hide_render = False

    # set rotation
    angleE = Euler([math.radians(DEdeg), 0, math.radians(RAdeg)], "XYZ")
    newStar.rotation_mode = 'QUATERNION'
    newStar.rotation_quaternion = angleE.to_quaternion()

    # set scale
    # Vmag = -2.5 log_10(l)
    l = pow(10, Vmag / -2.5)
    print(Vmag, l)
    scale = math.sqrt(l)
    newStar.scale = (scale, 1, scale)

    print(RAdeg, DEdeg, Vmag, newStar.rotation_quaternion, angleE)

with open("bsc5.txt") as file:
    for line in file:
        make_star(line)

starBase.hide_render = True

bpy.ops.wm.save_as_mainfile(filepath=os.path.abspath("stars.blend"))
