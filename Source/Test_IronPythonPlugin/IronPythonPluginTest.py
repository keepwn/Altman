import clr
clr.AddReference('Altman.Plugin')
from Altman.Plugin.Interface import IPluginInfo,IPluginSetting,IControlPlugin
import sys,os

class PluginInfo(IPluginInfo):
    @property
    def Name(self):
        return 'IronPythonPluginTest'
    @property
    def FileName(self):
        return 'IronPythonPluginTest.py'
    @property
    def Version(self):
        return '1.4.0' 
    @property
    def Author(self):
        return 'Keepwn'
    @property
    def Description(self):
        return 'this is a ironpython plugin demo.'

class PluginSetting(IPluginSetting):
    @property
    def IsAutoLoad(self):
        return False
    @property
    def IndexInList(self):
        return 1
    @property
    def LoadPath(self):
        return ''

@export(IPlugin)
class Plugin(IControlPlugin):
    def __init__(self):
        self.userControl = None
        self.pluginInfo = PluginInfo()
        self.pluginSetting = PluginSetting()

    @import_one(IHost)
    def import_host(self, host):
        self.host = host

    @property
    def PluginInfo(self):
        return self.pluginInfo
    
    @property
    def PluginSetting(self):
        return self.pluginSetting

    def Load(self):
        return True

    def Show(self, argv):
        #self._userControl = MyFirstPlugin(_host,data)
        #return self._userControl
        pass

    def Dispose(self):
        self.userControl = None