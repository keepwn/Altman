import clr
clr.AddReference('Altman.Plugin')
from Altman.Plugin import PluginServiceProvider
from Altman.Plugin.Interface import IService
import sys,os

@export(IService)
class Service(IService):
    def Load(self):
        PluginServiceProvider.RegisterService("ToHex", "Encode", hex_encode)
        PluginServiceProvider.RegisterService("FromHex", "Decode", hex_decode)
        return True

def hex_encode(args):
    return args[0].encode('hex')

def hex_decode(args):
    return args[0].decode('hex')


