def import_many(type, is_recomposable = True):
    def _(method):
        method.func_dict["imports"] = IronPythonImportDefinition(method.func_name, type, "ZeroOrOne", is_recomposable, True)
        return method
    return _

def import_one(type, is_recomposable = True):
    def _(method):
        method.func_dict["imports"] = IronPythonImportDefinition(method.func_name, type, "ExactlyOne", is_recomposable, True)
        return method
    return _

def export(type):
    def _(cls):
        exports = []
        try:
            exports = cls.__exports__
        except AttributeError:
            pass
        exports.append(type)
        cls.__exports__ = exports
        return cls
    return _